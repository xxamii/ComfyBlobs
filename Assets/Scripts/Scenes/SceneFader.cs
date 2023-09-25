using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        _animator.SetTrigger("fadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("fadeOut");
    }
}
