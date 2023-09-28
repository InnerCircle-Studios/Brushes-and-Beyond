using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    CharacterDirection lastDirection = CharacterDirection.Down;


    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;

    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;
    private bool isAttacking = false;
    private bool isShowing = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        ExecuteMove();
    }

     private bool CheckMove(Vector2 direction) { //Checks for collision aswell as move Character
        if(direction == Vector2.zero) return false;

        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset); // Checks for collision based on user input and rb

        if(count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); // Moves Character
            return true;
        }
        
        return false;
    }

void ExecuteMove()
{
    if (isAttacking) return;
    if (isShowing) return;
    bool success = movementInput != Vector2.zero &&
                   (CheckMove(movementInput) ||
                    CheckMove(new Vector2(movementInput.x, 0)) ||
                    CheckMove(new Vector2(0, movementInput.y)));

    if (success)
    {
        lastDirection = GetFacingDirection(); 
        PlayMoveAnimation(lastDirection);
    }
    else if (lastDirection != CharacterDirection.None)
    {
        PlayIdleAnimation(lastDirection);
    }
   // Debug.Log($"Movement Input: {movementInput}, Success: {success}, Last Direction: {lastDirection}");
}

void PlayMoveAnimation(CharacterDirection direction)
{
    switch (direction)
    {
        case CharacterDirection.Up:
            animator.Play("BackMove");
            break;
        case CharacterDirection.Down:
            animator.Play("ForwardMove");
            break;
        case CharacterDirection.Left:
            animator.Play("SideMove");
            spriteRenderer.flipX = true;  
            break;
        case CharacterDirection.Right:
            animator.Play("SideMove");
            spriteRenderer.flipX = false;
            break;
    }
}

void PlayIdleAnimation(CharacterDirection direction)
{
    switch (direction)
    {
        case CharacterDirection.Up:
            animator.Play("BackIdle");
            break;
        case CharacterDirection.Down:
            animator.Play("ForwardIdle");
            break;
        case CharacterDirection.Left:
            animator.Play("SideIdle");
            spriteRenderer.flipX = true;
            break;
        case CharacterDirection.Right:
            animator.Play("SideIdle");
            spriteRenderer.flipX = false;
            break;
    }
}

void OnMove(InputValue movementValue) {
     movementInput = movementValue.Get<Vector2>();
}


public enum CharacterDirection
{
    Left,
    Right,
    Up,
    Down,
    None
}


public CharacterDirection GetFacingDirection()
{
    if (movementInput.x > 0)
        return CharacterDirection.Right;
    else if (movementInput.x < 0)
        return CharacterDirection.Left;
    else if (movementInput.y > 0)
        return CharacterDirection.Up;
    else if (movementInput.y < 0)
        return CharacterDirection.Down;
    else
        return CharacterDirection.None; // If the character isn't moving.
}

void OnAttack(InputValue inputValue)
{
    // Check if the input was pressed (and not released)
    if (inputValue.isPressed)
    {
        PerformAttack();
    }
}


void PerformAttack()
{
    if (!isAttacking) 
    {
        CharacterDirection direction = (movementInput == Vector2.zero) ? lastDirection : GetFacingDirection();

        switch (direction)
        {
            case CharacterDirection.Up:
                animator.Play("BackSwordSwing");
                break;
            case CharacterDirection.Down:
                animator.Play("ForwardSwordSwing");
                break;
            case CharacterDirection.Left:
            case CharacterDirection.Right:
                animator.Play("SideSwordSwing");
                break;
            default:
                return; // For None, don't swing (this case is now less likely to happen but can be a safety check)
        }

        isAttacking = true;
        StartCoroutine(AnimationCooldown());
    }
}


IEnumerator AnimationCooldown()
{
    // Wait for the length of the animation or a set duration before allowing another attack
    if(isAttacking){
    yield return new WaitForSeconds(0.45f); // Assume 1 second attack duration; adjust as needed
    }
    if(isShowing){
    yield return new WaitForSeconds(2f); // Assume 1 second attack duration; adjust as needed
    }
    isAttacking = false;
    isShowing = false;
}

void OnShow(InputValue inputValue){
        if (inputValue.isPressed)
    {
        ShowItem();
    }

}

void ShowItem(){

    if(!isShowing){
        animator.Play("ShowBrush");
    }
    isShowing = true;
    StartCoroutine(AnimationCooldown());
}

}
