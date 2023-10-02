using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerStateMachine : MonoBehaviour
{
    //User variables
    [SerializeField] private float _force = 10;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 15;

     public TextMeshProUGUI stateTextMeshPro;

    //Reference variables
    PlayerInput _playerInput;
    private Rigidbody2D _rb;

    //Constant variables
    private int _zero = 0;

    //Movement variables
    Vector2 _currentMovementInput;
    Vector3 _appliedMovement;
    private bool _isMovementPressed = false;
    private bool _isRunningPressed = false;
    //Graphic Variables
    CharacterDirection _currentDirection;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    //Attack variables
    private bool _isAttackPressed = false;

    //State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //Getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }
    public CharacterDirection CurrentDirection{get { return _currentDirection; }set { _currentDirection = value; }}
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }
    //public LayerMask GroundMask { get { return groundMask; } set { groundMask = value; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsRunningPressed { get { return _isRunningPressed; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float SprintSpeed { get { return _sprintSpeed; } set { _sprintSpeed = value; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } set { _appliedMovement = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
    void Awake()
    {
        //Initiate reference variables
        _playerInput = new PlayerInput();
        //State setup
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

    }
    void Start()
    {
    _rb = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState();
        if(stateTextMeshPro != null) 
        {
            stateTextMeshPro.text = _currentState.GetType().Name;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
        if(_currentMovementInput.x > 0) 
    CurrentDirection = CharacterDirection.Right;
else if(_currentMovementInput.x < 0) 
    CurrentDirection = CharacterDirection.Left;
else if(_currentMovementInput.y > 0) 
    CurrentDirection = CharacterDirection.Up;
else if(_currentMovementInput.y < 0) 
    CurrentDirection = CharacterDirection.Down;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    } 

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunningPressed = context.ReadValueAsButton();
    }

    public enum CharacterDirection 
{
    Up,
    Down,
    Left,
    Right
}
}
