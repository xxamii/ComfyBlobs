using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GateDoor
{
    
    private LevelManager _level;

    protected override void Start()
    {
        base.Start();
        _level = FindObjectOfType<LevelManager>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_isOpened)
        {
            Eater eater = other.GetComponent<Eater>();

            if (eater != null)
            {
                _level.LoadNextLevel();
            }
        }
    }
}
