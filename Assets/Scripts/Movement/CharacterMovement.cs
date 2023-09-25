using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float _movingSpeed;
    public int _facingDirection;

    private Rigidbody2D _rigidBody;
    private Transform _transform;

    private bool _canMove = true;
    public bool CanMove
    { 
        get
        {
            return _canMove;
        }

        set
        {
            _canMove = value;

            if (!value)
            {
                _movingDirection = Vector2.zero;
                _rigidBody.velocity = Vector2.zero;
            }
        }
    }

    private Vector2 _movingDirection;

    public Vector2 MovingDirection
    {
        get
        {
            return _movingDirection;
        }

        set
        {
            _movingDirection = value;
            TryFlip();
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float desiredSpeed = _movingSpeed;

        if (_movingDirection.x != 0f && _movingDirection.y != 0f)
        {
            desiredSpeed /= Mathf.Sqrt(2);
        }

        _rigidBody.velocity = _movingDirection * desiredSpeed;
    }

    private void TryFlip()
    {
        if ((_movingDirection.x == 1f && _facingDirection == -1) || (_movingDirection.x == -1f && _facingDirection == 1))
        {
            _transform.Rotate(0, 180, 0);
            _facingDirection = -_facingDirection;
        }
    }
}
