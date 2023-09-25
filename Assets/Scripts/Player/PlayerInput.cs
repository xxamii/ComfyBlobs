using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public SlimeAnimator _animator;

    private CharacterMovement _movement;
    private Eater _eater;

    private bool _canInput = true;
    public bool CanInput
    {
        get
        {
            return _canInput;
        }

        set
        {
            _canInput = value;
        }
    }

    private void Start()
    {
        _movement = GetComponent<CharacterMovement>();
        _eater = GetComponent<Eater>();
    }

    private void Update()
    {
        if (_canInput)
        {
            MovementInput();
            SplitInput();
            ChangeSlimeTypeToEject();
        }
    }

    private void MovementInput()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movement.MovingDirection = direction;

        if (direction != Vector2.zero)
        {
            _animator.SetAnimationBool("isMoving", true);
        }
        else
        {
            _animator.SetAnimationBool("isMoving", false);
        }
    }

    private void SplitInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _eater.StartSplit();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            _eater.StopSplit();
        }
    }

    private void ChangeSlimeTypeToEject()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _eater.SelectNextSlimeTypeToEject();
        }
    }
}
