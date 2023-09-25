using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFactory : MonoBehaviour
{
    public Slime _greenSlimePrefab;
    public Slime _blueSlimePrefab;

    private Vector3 _currentInstancePosition;
    private Vector3 _currentInstanceRotation;

    private Slime _currentlyInstantiatedSlime;

    public void InstantiateSlimeOfType(SlimeType.Type type, Vector3 position, Vector3 rotation)
    {
        _currentInstancePosition = position;
        _currentInstanceRotation = rotation;

        switch (type)
        {
            case SlimeType.Type.Green:
                InstantiateGreenSlime();
                break;
            case SlimeType.Type.Blue:
                InstantiateBlueSlime();
                break;
        }
    }

    public Slime GetCurrentlyInstantiatedSlime()
    {
        return _currentlyInstantiatedSlime;
    }

    private void InstantiateGreenSlime()
    {
        _currentlyInstantiatedSlime = Instantiate(_greenSlimePrefab, _currentInstancePosition, Quaternion.Euler(_currentInstanceRotation));
    }

    private void InstantiateBlueSlime()
    {
        _currentlyInstantiatedSlime = Instantiate(_blueSlimePrefab, _currentInstancePosition, Quaternion.Euler(_currentInstanceRotation));
    }
}
