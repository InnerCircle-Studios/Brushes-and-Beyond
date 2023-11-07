using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStateMachine : MonoBehaviour {
    //User variables t
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 15;
    [SerializeField] private float _dashDistance = 10.0f;
    [SerializeField] private UnityEvent dialogueTrigger;
    [SerializeField] private UnityEvent gameStartDialogueTrigger;
    [SerializeField] private UnityEvent gameItem;
    [SerializeField] private UnityEvent gameBlockade;
    [SerializeField] private UnityEvent nextMessageTrigger;
    [SerializeField] private UnityEvent tutorialMovement;
    [SerializeField] private UnityEvent tutorialRun;
    [SerializeField] private UnityEvent tutorialInteract;
    [SerializeField] private UnityEvent tutorialAttack;
    [SerializeField] private UnityEvent playerDeath;
    public TextMeshProUGUI stateTextMeshPro;

    //Reference variables
    PlayerInput _playerInput;
    private Rigidbody2D _rb;

    //Constant variables
    private int _zero = 0;

    // Tutorial variables
    private bool _tutorialMovement = false;
    private bool _tutorialRun = false;
    private bool _tutorialInteract = false;
    private bool _tutorialAttack = false;

    // Dash variables
    private float _dashDuration = 0.2f;
    private bool _isDashPressed = false;
    private float _lastDashTime = -10.0f;
    private const float _dashCooldown = 10.0f;
    public Slider dashCooldownSlider;

    //Show variables
    private bool _isShowPressed = false;

    //Movement variables
    Vector2 _currentMovementInput;
    Vector3 _appliedMovement;
    private bool _isMovementPressed = false;
    private bool _isRunningPressed = false;

    //Graphic Variables
    CharacterDirection _currentDirection = CharacterDirection.Down;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    //Interact variables
    private bool _isInteractPressed = false;
    private bool _dialogueTrigger = false;
    private bool _playerIsInDialogue = false;
    private bool _nearNPC = false;
    private bool _nearItem = false;
    private bool _nearBlockade = false;

    //Player variables
    private AttributeManager2 _attributeManager;
    private bool _isAlive = true;

    //Attack variables
    private bool _isAttackPressed = false;

    //State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //Getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }
    public CharacterDirection CurrentDirection { get { return _currentDirection; } set { _currentDirection = value; } }
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsRunningPressed { get { return _isRunningPressed; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float SprintSpeed { get { return _sprintSpeed; } set { _sprintSpeed = value; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } set { _appliedMovement = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
    public float DashDistance { get { return _dashDistance; } set { _dashDistance = value; } }
    public float DashDuration { get { return _dashDuration; } set { _dashDuration = value; } }
    public bool IsDashPressed { get { return _isDashPressed; } set { _isDashPressed = value; } }
    public bool IsInteractPressed { get { return _isInteractPressed; } set { _isInteractPressed = value; } }
    public bool DialogueTrigger { get { return _dialogueTrigger; } set { _dialogueTrigger = value; } }
    public bool PlayerIsInDialogue { get { return _playerIsInDialogue; } set { _playerIsInDialogue = value; } }
    public float LastDashTime { get { return _lastDashTime; } set { _lastDashTime = value; } }
    public float DashCooldown { get { return _dashCooldown; } }
    public bool IsShowPressed { get { return _isShowPressed; } set { _isShowPressed = value; } }
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }

    void Awake() {
        //State setup
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentState = _states.Dialogue();
        gameStartDialogueTrigger.Invoke();
        _attributeManager = gameObject.GetComponent<AttributeManager2>();
    }


    void Update() {
        DialogueCheck();
        PlayerHPCheck();
        HandleCooldownUI();
        _currentState.UpdateState();
        if (stateTextMeshPro != null) {
            stateTextMeshPro.text = _currentState.GetType().Name;
        }
    }


    public void OnMove(InputAction.CallbackContext context) {
        _currentMovementInput = context.ReadValue<Vector2>();
        if(_tutorialMovement == false && _currentMovementInput.x != _zero || _currentMovementInput.y != _zero) {
            tutorialMovement.Invoke();
            _tutorialMovement = true;
        }
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
        if (_currentMovementInput.x > 0) {
            CurrentDirection = CharacterDirection.Right;
        }
        else if (_currentMovementInput.x < 0) {
            CurrentDirection = CharacterDirection.Left;
        }
        else if (_currentMovementInput.y > 0) {
            CurrentDirection = CharacterDirection.Up;
        }
        else if (_currentMovementInput.y < 0) {
            CurrentDirection = CharacterDirection.Down;
        }

    }

    public void OnJump(InputAction.CallbackContext context) {
        _isAttackPressed = context.ReadValueAsButton();
        if (!_tutorialAttack && _isAttackPressed) {
            tutorialAttack.Invoke();
            _tutorialAttack = true;
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        _isRunningPressed = context.ReadValueAsButton();
        if (!_tutorialRun && _isRunningPressed && _isMovementPressed) {
            tutorialRun.Invoke();
            _tutorialRun = true;
        }
    }

    public void OnShow(InputAction.CallbackContext context) {
        _isShowPressed = context.ReadValueAsButton();
    }

    public void OnDash(InputAction.CallbackContext context) {
        _isDashPressed = false;
        if (Time.time - _lastDashTime >= _dashCooldown) {
            _isDashPressed = context.ReadValueAsButton();
        }
    }
    public void OnInteract(InputAction.CallbackContext context) {
        _isInteractPressed = context.ReadValueAsButton();
        if (!_tutorialInteract && _isInteractPressed) {
            tutorialInteract.Invoke();
            _tutorialInteract = true;
        }
        if (_isInteractPressed && _nearNPC) { //Check if interacting with NPC
            _dialogueTrigger = true;
            if (!_playerIsInDialogue) {
                dialogueTrigger.Invoke();
            }
            if (_playerIsInDialogue) {
                nextMessageTrigger.Invoke();
            }

        }
        else {
            _dialogueTrigger = false;
        }
        if (_isInteractPressed && _nearItem) { //Check if interacting with item
            gameItem.Invoke();
            DestroyItem();
        }
        if (_isInteractPressed && _nearBlockade) { //Check if interacting with blockade
            gameBlockade.Invoke();
        }
    }


    public void UsingPaints() {
        _isShowPressed = true;
    }

    public void DialogueCheck() {
        if (DialogueManager.isActive) {
            _playerIsInDialogue = true;
        }
        else {
            _playerIsInDialogue = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("NPC")) {
            _nearNPC = true;
        }
        if (collision.gameObject.CompareTag("Item")) {
            _nearItem = true;
        }
        if (collision.gameObject.CompareTag("Blockade")) {
            _nearBlockade = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("NPC")) {
            _nearNPC = false;
        }
        if (collision.gameObject.CompareTag("Item")) {
            _nearItem = false;
        }
        if (collision.gameObject.CompareTag("Blockade")) {
            _nearBlockade = false;
        }
    }

    public void HandleCooldownUI() {
        float timeSinceLastDash = Time.time - _lastDashTime;
        float cooldownProgress = Mathf.Clamp01(timeSinceLastDash / _dashCooldown);
        dashCooldownSlider.value = cooldownProgress;
    }

    public enum CharacterDirection {
        Up,
        Down,
        Left,
        Right
    }
    private void DestroyItem() {
        // Check if the player is near an item with the "Item" tag
        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Item"));
        if (itemCollider) {
            Destroy(itemCollider.gameObject);
        }
    }

    private void PlayerHPCheck(){
        if(_attributeManager.GetAttributes().CurrentHealth < 1){
            _isAlive = false;
            playerDeath.Invoke();
        }
    }
}
