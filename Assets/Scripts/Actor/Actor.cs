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
        sRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.2f);
        sRenderer.color = startColor;
    }

    public IAttrubuteManager GetAttrubuteManager() {
        return _AttributeManager;
    }

    public ICombat GetCombat() {
        return _Combat;
    }

    public IAnimator GetAnimator() {
        return _Animator;
    }

    public void HandleWalk(Vector2 desiredMovement) {
        _RigidBody.MovePosition(_RigidBody.position + desiredMovement);
    }

    protected Rigidbody2D GetRigidBody() {
        return _RigidBody;
    }

    private IAttrubuteManager _AttributeManager;

    [SerializeField] private CharacterAttributes attributes;

    private Rigidbody2D _RigidBody;

    private ICombat _Combat;
    private IAnimator _Animator;
}