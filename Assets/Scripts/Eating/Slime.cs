using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Weight
{
    public SlimeType.Type _type;
    public LayerMask _collidersLayers;
    public Collider2D[] _colliders;
    public SlimeAnimator _animator;
    public bool _isStartIdle;

    public SlimeType.Type Type => _type;

    private Eater _currentEater;
    private bool _isAlive = true;

    protected override void Start()
    {
        base.Start();

        _animator.SetAnimationBool("idle", _isStartIdle);

        if (IsInsideCollider())
            SwitchOffColliders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _currentEater != null && _isAlive)
        {
            if (_currentEater.GetCurrentWeight() >= GetCurrentWeight())
            {
                _currentEater.Eat(this);
                OnEaten(_currentEater);
                DestroySelf();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrySetSetCurrentEater(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TrySetSetCurrentEater(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TryRemoveCurrentEater(other);
    }

    public void SplitWithWeight(float weight)
    {
        SetWeight(weight);
        InitScale();

        _animator.PlaySplitedAnimation();
    }

    public void SplitInsideCollider(float weight)
    {
        SplitWithWeight(weight);
        SwitchOffColliders();
    }

    protected virtual void OnEaten(Eater eater)
    {
        return;
    }

    private bool IsInsideCollider()
    {
        float radius = CalculateRadius();
        Collider2D[] insideCollider = Physics2D.OverlapCircleAll(_transform.position, radius, _collidersLayers.value);

        return insideCollider.Length > 0;
    }

    private float CalculateRadius()
    {
        return (CalculateScaleFromWeight(GetCurrentWeight()) * 0.6f) / 2f;
    }

    private void SwitchOnColliders()
    {
        foreach (Collider2D collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    private void SwitchOffColliders()
    {
        foreach (Collider2D collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    private void DestroySelf()
    {
        _isAlive = false;
        FireOnDestroySelf();
        _animator.PlaySuckedAnimation();
        Destroy(gameObject, 0.5f);
    }

    private void TrySetSetCurrentEater(Collider2D other)
    {
        if (_currentEater == null)
        {
            Eater eater = other.GetComponent<Eater>();

            if (eater != null)
            {
                _currentEater = eater;
            }
        }
    }

    private void TryRemoveCurrentEater(Collider2D other)
    {
        if (_currentEater != null)
        {
            Eater eater = other.GetComponent<Eater>();

            if (eater != null)
            {
                _currentEater = null;
                SwitchOnColliders();
            }
        }
    }
}
