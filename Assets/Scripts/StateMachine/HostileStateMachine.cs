using UnityEngine;

public class HostileStateMachine : StateMachine
{   
    public HostileStateMachine(Actor actor, Player player, bool isMelee) : base(actor)
    {
        AddState(new HostileIdleState("HostileIdleState", this));
        AddState(new HostileAttackState("HostileAttackState", this));
        AddState(new HostileDeathState("HostileDeathState", this));
        AddState(new HostileWalkState("HostileWalkState", this));

        ChangeState(GetState("HostileIdleState"));

        InitSwitchCases();

        _isMelee.Value = isMelee;
        _Hostile = GetActor() as Hostile;
    }
    

    public void CheckPlayerInRange()
    {
        float distance = Vector2.Distance(GetActor().transform.position, _Hostile.GetPlayer().transform.position);

        if (distance < _hostileRange)
        {
            _isInRange.Value = true;
        }
        else
        {
            _isInRange.Value = false;
        }  
    }

    public void CheckPlayerInAttackRange()
    {
        float distance = Vector2.Distance(GetActor().transform.position, _Hostile.GetPlayer().transform.position);

        if (distance < _attackRange)
        {
            _isInAttackRange.Value = true;
        }
        else
        {
            _isInAttackRange.Value = false;
        }  
    }

    public Vector2 _currentMovement = new Vector2();
    public float _hostileRange = 10f;
    public float _attackRange = 3f;

    public BoolWrapper _isInRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isInAttackRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isMelee{ get; set; } = new BoolWrapper(false);
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isIdle { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isShooting { get; set; } = new BoolWrapper(false);

    public Hostile _Hostile;
}