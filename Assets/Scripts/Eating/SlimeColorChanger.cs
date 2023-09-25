using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeColorChanger : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    private Eater _eater;

    private void Start()
    {
        _eater = GetComponent<Eater>();
        _eater.OnEjectSlimeTypeSelected += ChangeSlimeColorToType;
    }

    private void OnDisable()
    {
        _eater.OnEjectSlimeTypeSelected -= ChangeSlimeColorToType;
    }

    private void ChangeSlimeColorToType(SlimeType.Type type)
    {
        _spriteRenderer.color = SlimeType.GetTypeColor(type);
    }
}
