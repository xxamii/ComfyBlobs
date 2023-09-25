using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{   
    
    private PickupAnimation _pickupAnim;
    private Collider2D _collider;
    private GameObject _player;
    
    protected virtual void Start()
    {
        _pickupAnim = GetComponent<PickupAnimation>();
        _collider = GetComponentInChildren<Collider2D>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Eater player = other.GetComponent<Eater>();
        if (player != null)
        {
            Collect();
        }
    }

    protected void Collect()
    {
        _collider.enabled = false;
        _pickupAnim.Disappear();
        _pickupAnim.StartFollowPlayer();
        // TODO Play pickup sound
        OnTouch();
    }

    public void OnPickupAnimationComplete()
    {
        OnPickup();
        Destroy(gameObject);
    }

    public virtual void OnTouch()
    {
        
    }

    public virtual void OnPickup()
    {
        Debug.Log("Picked up!");
    }
    
}
