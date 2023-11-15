using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public void Awake()
    {
        _AttributeManager = new AttributeManager(attributes);

        _Combat = new Combat();
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

    private IAttrubuteManager _AttributeManager;
    [SerializeField] private CharacterAttributes attributes;

    private ICombat _Combat;
}