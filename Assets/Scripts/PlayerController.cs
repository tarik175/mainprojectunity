using System;
using System.Runtime.InteropServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public float MoveForce { get; private set; } = 250f;
    [field: SerializeField] public CharacterState Idle { get; private set; }
    [field: SerializeField] public CharacterState Walk { get; private set; }
    [field: SerializeField] public CharacterState Use { get; private set; }
    [field: SerializeField] public StateAnimationSetDictionary StateAnimations { get; private set; }
    [field: SerializeField] public float WalkVelocityThreshold { get; private set; } = 0.05f;

    public CharacterState CurrentState
    {
        get
        {
            return _currentState;
        }
        private set
        {
            if (_currentState != value)
            {
                _currentState = value;
                ChangeClip();
                timeToEndAnimation = _currentClip.length;
            }
        }
    }

    private Vector2 _axisinput = Vector2.zero;
    private Rigidbody2D _rb;
    private Animator _animator;
    private CharacterState _currentState;
    private AnimationClip _currentClip;
    private Vector2 _facingDir;
    private float timeToEndAnimation = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentState = Idle;
    }
    private void Update()
    {
        timeToEndAnimation = Mathf.Max(timeToEndAnimation - Time.deltaTime, 0);
        if (_currentState.CanExitWhilePlaying || timeToEndAnimation <= 0)
        {
            if (_axisinput != Vector2.zero && _rb.linearVelocity.magnitude > WalkVelocityThreshold)
            {
                _currentState = Walk;
            }
            else
            {
                _currentState = Idle;
            }

            ChangeClip();

        }
        
    }

    private void ChangeClip()
    {
        AnimationClip expectedClip = StateAnimations.GetFacingClipFromState(_currentState, _facingDir);
        if (_currentClip == null || _currentClip != expectedClip)
        {
            _animator.Play(expectedClip.name);
            _currentClip = expectedClip;
        }
    }

    private void FixedUpdate()
    {
        if (_currentState.canMove)
        {
        Vector2 moveForce = _axisinput * MoveForce * Time.fixedDeltaTime;
        _rb.AddForce(moveForce);
        }
    }
    private void OnMove(InputValue value)
    {
        _axisinput = value.Get<Vector2>();
        
        if (_axisinput != Vector2.zero)
        {
            _facingDir = _axisinput;
        }
    }

    private void OnUse(InputValue value)
    {
        CurrentState = Use;
    }
}
