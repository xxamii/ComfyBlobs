using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : Weight
{
    public LayerMask _groundLayer;
    public float _groundCheckRadius;

    public float _minWeight;
    public Transform _splitPoint;
    public float _splitWeightCalculationDelay;

    public SlimeAnimator _animator;

    private Dictionary<SlimeType.Type, int> _consumedSlimes = new Dictionary<SlimeType.Type, int>(); // <Slime type, total weight of the consumed slimes of the type>
    private SlimeType.Type _currentSlimeTypeToEject;
    private SlimeType.Type _defaultSlimeTypeToEject = SlimeType.Type.Blue;

    public event Action OnSplitWeightChange;
    public event Action<SlimeType.Type> OnEjectSlimeTypeSelected;

    private SlimeFactory _slimeFactory;

    private Coroutine _currentSplitRoutine;

    private float _currentSplitWeight;

    public float CurrentSplitWeight
    {
        get
        {
            return _currentSplitWeight;
        }
        set
        {
            if (!IsSplitWeightValid(value))
            {
                _currentSplitWeight = 0f;
            }
            else
            {
                _currentSplitWeight = value;
            }
            OnSplitWeightChange?.Invoke();
        }
    }

    protected override void Start()
    {
        base.Start();

        _slimeFactory = GetComponent<SlimeFactory>();

        CurrentSplitWeight = CalculateSplitWeight();
        SelectSlimeTypeToEject(_defaultSlimeTypeToEject);
    }

    public void Eat(Slime slime)
    {
        AddSlime(slime);
        PlayEatAnimation();
    }

    public void StartSplit()
    {
        CurrentSplitWeight = CalculateSplitWeight();

        _currentSplitRoutine = StartCoroutine(DecreaseSplitWeightRoutine());
    }

    public void StopSplit()
    {
        StopCoroutine(_currentSplitRoutine);

        if (!IsSplitWeightValid(CurrentSplitWeight))
        {
            return;
        }

        InstantiateSplitSlime();
    }

    public void SelectNextSlimeTypeToEject()
    {
        SlimeType.Type newSlimeTypeToEject = SlimeType.GetNextSlimeType(_currentSlimeTypeToEject);

        if (!_consumedSlimes.ContainsKey(newSlimeTypeToEject))
        {
            if (_consumedSlimes.Count == 0)
            {
                newSlimeTypeToEject = _defaultSlimeTypeToEject;
            }
            else
            {
                newSlimeTypeToEject = _consumedSlimes.First().Key;
            }
        }

        SelectSlimeTypeToEject(newSlimeTypeToEject);
    }

    private void SelectSlimeTypeToEject(SlimeType.Type type)
    {
        _currentSlimeTypeToEject = type;
        OnEjectSlimeTypeSelected?.Invoke(_currentSlimeTypeToEject);
    }

    private void AddOrIncreaseSlimeType(Slime slime)
    {
        if (_consumedSlimes.ContainsKey(slime.Type))
        {
            _consumedSlimes[slime.Type] += (int)slime.GetCurrentWeight();
        }
        else
        {
            _consumedSlimes.Add(slime.Type, (int)slime.GetCurrentWeight());
            SelectSlimeTypeToEject(slime.Type); // SRP violated
        }
    }

    private void RemoveOrDeleteSlimeType(Slime slime)
    {
        if (_consumedSlimes.ContainsKey(slime.Type))
        {
            _consumedSlimes[slime.Type] -= (int)slime.GetCurrentWeight();

            if (_consumedSlimes[slime.Type] <= 0)
            {
                _consumedSlimes.Remove(slime.Type);
                SelectNextSlimeTypeToEject(); // SRP violated
            }
        }
        else
        {
            Debug.LogError(name + ": Trying to remove SlimeType that was not consumed");
        }
    }

    private IEnumerator DecreaseSplitWeightRoutine()
    {
        int selectedSlimeWeight = (int)_consumedSlimes[_currentSlimeTypeToEject];
        int halfCurrentWeight = (int)GetCurrentWeight() / 2;
        CurrentSplitWeight = MathUtils.FindClosestPowerOfTwoOrOne(Mathf.Min(selectedSlimeWeight - 1, halfCurrentWeight));

        while (true)
        {
            yield return new WaitForSeconds(_splitWeightCalculationDelay);

            CurrentSplitWeight = MathUtils.FindClosestPowerOfTwoOrOne((int)CurrentSplitWeight - 1);
        }
    }

    private void InstantiateSplitSlime()
    {
        Vector2 slimeSplitPosition = CalculateSplitSlimePosition();
        Vector3 slimeSplitRotation = CalculateSplitSlimeRotation();

        _slimeFactory.InstantiateSlimeOfType(_currentSlimeTypeToEject, slimeSplitPosition, slimeSplitRotation);
        Slime slime = _slimeFactory.GetCurrentlyInstantiatedSlime();

        slime.SplitWithWeight(CurrentSplitWeight);
        
        SubtractSlime(slime);
        PlaySplitAnimation();
    }

    private Vector2 CalculateSplitSlimePosition()
    {
        float slimeRadius = CalculateSplitSlimeRadius();
        float distanceToSplitPoint = Vector2.Distance(_transform.position, _splitPoint.position);
        float splitPointColliderCheckDistance = distanceToSplitPoint + slimeRadius;
        RaycastHit2D splitPointColliderCheck = Physics2D.Raycast(_transform.position, _transform.right, splitPointColliderCheckDistance, _groundLayer.value);

        if (splitPointColliderCheck)
        {
            float distanceToCollider = splitPointColliderCheck.distance;
            float xDistanceDifference = (distanceToCollider - slimeRadius - 0.1f) * _transform.right.x;
            Vector2 desiredSplitPoint = new Vector2(_transform.position.x + xDistanceDifference, _splitPoint.position.y);
            return desiredSplitPoint;
        }

        return _splitPoint.position;
    }

    private Vector3 CalculateSplitSlimeRotation()
    {
        Vector3 desiredSplitRotation = _splitPoint.eulerAngles;
        desiredSplitRotation.y += 180f;
        return desiredSplitRotation;
    }

    private float CalculateSplitSlimeRadius()
    {
        return (CalculateScaleFromWeight(CurrentSplitWeight) * 0.6f) / 2f;
    }

    private void SubtractSlime(Slime slime)
    {
        float slimeWeight = slime.GetCurrentWeight();
        SubtractWeightFromCurrent(slimeWeight);
        RemoveOrDeleteSlimeType(slime);
        CurrentSplitWeight = CalculateSplitWeight();
    }

    private void AddSlime(Slime slime)
    {
        float slimeWeight = slime.GetCurrentWeight();
        AddWeightToCurrent(slimeWeight);
        AddOrIncreaseSlimeType(slime);
        CurrentSplitWeight = CalculateSplitWeight();
    }

    private void PlaySplitAnimation()
    {
        _animator.PresetScale(CalculateScaleFromWeight(GetCurrentWeight()));
        _animator.PlaySplitAnimation();
    }

    private void PlayEatAnimation()
    {
        _animator.PresetScale(CalculateScaleFromWeight(GetCurrentWeight()));
        _animator.PlaySuckAnimation();
    }

    private float CalculateSplitWeight()
    {
        if (!_consumedSlimes.ContainsKey(_currentSlimeTypeToEject))
        {
            return 0f;
        }

        int currentChosenSlimeWeight = _consumedSlimes[_currentSlimeTypeToEject];
        int halfOfCurrentWeight = (int)GetCurrentWeight() / 2;
        return MathUtils.FindClosestPowerOfTwoOrOne(Mathf.Min(currentChosenSlimeWeight, halfOfCurrentWeight));
    }

    private bool IsSplitWeightValid(float splitWeight)
    {
        return splitWeight >= 1f && GetCurrentWeight() - splitWeight > 0f;
    }
}
