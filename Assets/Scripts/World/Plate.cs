using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public AudioClip _pressSound;
    public float _sizeToPress;

    public event Action OnToggle;

    public bool IsPressed => _isPressed;

    private Animator _animator;
    private AudioPlayer _audioPlayer;

    private List<Weight> _sizeOnPlateList;
    private float _sizeOnPlate;
    private bool _isPressed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioPlayer = AudioPlayer.Instance;

        _sizeOnPlateList = new List<Weight>();
    }

    public void PlayPressSound()
    {
        _audioPlayer.PlayEffect(_pressSound);
    }

    // if a slime or the player steps on the plate, add their size to the whole amount
    private void OnTriggerEnter2D(Collider2D other)
    {
        Weight weight = other.GetComponent<Weight>();

        if (weight)
        {
            _sizeOnPlateList.Add(weight);
            CalculateWeight();
        }
    }

    // if a slime or the player steps off the plate, remove their size from the whole amount
    private void OnTriggerExit2D(Collider2D other)
    {
        Weight weight = other.GetComponent<Weight>();

        if (weight)
        {
            _sizeOnPlateList.Remove(weight);
            CalculateWeight();
        }
    }

    private void CalculateWeight()
    {
        float total = 0f;

        for (int i = _sizeOnPlateList.Count - 1; i > -1; i--)
        {
            Weight weight = _sizeOnPlateList[i];

            if (weight != null)
            {
                total += weight.GetCurrentWeight();
            }
            else
            {
                _sizeOnPlateList.RemoveAt(i);
            }
        }

        _sizeOnPlate = total;

        TryOpen();
        TryClose();
    }

    private void TryOpen()
    {
        if (_sizeOnPlate >= _sizeToPress && !_isPressed)
        {
            _animator.SetTrigger("press");
            _isPressed = true;
            OnToggle?.Invoke();
        }
    }

    private void TryClose()
    {
        if (_sizeOnPlate < _sizeToPress && _isPressed)
        {
            _animator.SetTrigger("leave");
            _isPressed = false;
            OnToggle?.Invoke();
        }
    }
}
