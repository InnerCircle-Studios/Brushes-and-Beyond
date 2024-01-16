using System;
using System.Collections;

using UnityEngine;

public abstract class Actor : MonoBehaviour {
    public void Awake() {

        _AttributeManager = new AttributeManager(attributes);

        _Combat = new Combat();

        _Animator = new AnimationManager(GetComponent<Animator>());

        _RigidBody = GetComponent<Rigidbody2D>();
    }

    public abstract void Start();

    public abstract void Update();

    public abstract void HandleMeleeAttack();

    public abstract void HandleRangedAttack();

    public IEnumerator FlashSpriteOnHit(SpriteRenderer sRenderer) {
        Color startColor = sRenderer.color;
        if (startColor != Color.gray) {
            sRenderer.color = Color.gray;
            yield return new WaitForSecondsRealtime(0.2f);

            // Handles an edge case where the enemy has died before the sprite could be reset
            if (sRenderer != null) {
                sRenderer.color = startColor;
            }
        }
    }

    public IAttributeManager GetAttrubuteManager() {
        return _AttributeManager;
    }

    public ICombat GetCombat() {
        return _Combat;
    }

    public IAnimator GetAnimator() {
        return _Animator;
    }

    public Rigidbody2D GetRigidBody() {
        return _RigidBody;
    }

    public GameManager GetGameManager() {
        return GameManager.Instance;
    }

    public void HandleWalk(Vector2 desiredMovement) {
        _RigidBody.MovePosition(_RigidBody.position + desiredMovement);
    }

    public void Dash(Vector2 currentInput) {
        _RigidBody.AddForce(currentInput.normalized * 10f, ForceMode2D.Impulse);
    }

    public void Knockback(Vector2 direction) {
        // Debug.Log(direction);
        _RigidBody.AddForce(direction * 5f, ForceMode2D.Impulse);
    }

    protected IAttributeManager _AttributeManager;

    [SerializeField] protected CharacterAttributes attributes;

    private Rigidbody2D _RigidBody;

    private ICombat _Combat;
    private IAnimator _Animator;
}