using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public void Awake()
    {
        _AttributeManager = new AttributeManager(attributes);

        _Combat = new Combat();
        
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    public abstract void Start();

    public abstract void Update();

    public abstract void HandleMeleeAttack();

    public abstract void HandleRangedAttack();

    public IAttrubuteManager GetAttrubuteManager()
    {
        return _AttributeManager;
    }

    public  ICombat GetCombat()
    {
        return _Combat;
    }

    public void HandleWalk(Vector2 desiredMovement)
    {
        _RigidBody.MovePosition(_RigidBody.position + desiredMovement);
    }

    private IAttrubuteManager _AttributeManager;
    [SerializeField] private CharacterAttributes attributes;

    private Rigidbody2D _RigidBody;

    private ICombat _Combat;
}