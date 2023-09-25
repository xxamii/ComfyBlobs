using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public float _currentWeight;

    protected Transform _transform;

    public event Action OnDestroySelf;

    protected void Awake()
    {
        _transform = transform;
    }

    protected virtual void Start()
    {
        InitScale();
    }

    public float GetCurrentWeight()
    {
        return _currentWeight;
    }

    protected void SetWeight(float weight)
    {
        _currentWeight = weight;
    }

    public void AddWeightToCurrent(float amount)
    {
        _currentWeight += amount;
    }

    public void SubtractWeightFromCurrent(float amount)
    {
        _currentWeight -= amount;
    }

    protected void FireOnDestroySelf()
    {
        OnDestroySelf?.Invoke();
    }

    protected void InitScale()
    {
        int scale = CalculateScaleFromWeight(_currentWeight);
        _transform.localScale = new Vector3(scale, scale, 1);
    }

    protected int CalculateScaleFromWeight(float weight)
    {
        int scale = 1;

        for (int i = 2; i <= weight; i++)
        {
            if (Mathf.IsPowerOfTwo(i))
            {
                scale++;
            }
        }

        return scale;
    }
}
