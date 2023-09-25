using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    public float followSpeed = 1f;
    public GameObject shadow;
    private Animator _anim;
    private bool _isFollowingPlayer;
    private Transform _player;

    protected void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _player = FindObjectOfType<Eater>().transform;
        _isFollowingPlayer = false;
    }
    
    protected void Update()
    {
        ProcessFollowPlayer();
    }
    
    public void StartFollowPlayer()
    {
        _isFollowingPlayer = true;
    }
    
    private void ProcessFollowPlayer()
    {
        if (!_isFollowingPlayer)
            return;
        
        Vector3 delta = transform.position - _player.position;
        float magn = Math.Min(delta.magnitude, followSpeed);
        transform.position -= delta.normalized * magn;
    }

    public void Disappear()
    {
        _anim.SetTrigger("Disappear");
    }
    
    // Called from animation to control shadow
    public void ShowShadow()
    {
        shadow.SetActive(true);
    }

    // Called from animation to control shadow
    public void HideShadow()
    {
        shadow.SetActive(false);
    }
}
