using UnityEngine;

public class HostileStateMachine : StateMachine
{   
    public HostileStateMachine(Actor actor, Player player) : base(actor)
    {
        AddState(new HostileAttackState("HostileAttackState", this));
        AddState(new HostileDeathState("HostileDeathState", this));
        AddState(new HostileIdleState("HostileIdleState", this));
        AddState(new HostileWalkState("HostileWalkState", this));

        ChangeState(GetState("HostileIdleState"));

        InitSwitchCases();

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

    public Vector2 _currentMovement = new Vector2();
    public float _hostileRange = 10f;

    public BoolWrapper _isInRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsInAttackRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isIdle { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isShooting { get; set; } = new BoolWrapper(false);

    private Hostile _Hostile;

}