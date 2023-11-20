public class HostileStateMachine : StateMachine
{   
    public HostileStateMachine(Actor actor) : base(actor)
    {
        AddState(new HostileAttackState("HostileAttackState", this));
        AddState(new HostileDeathState("HostileDeathState", this));
        AddState(new HostileIdleState("HostileIdleState", this));
        AddState(new HostileWalkState("HostileWalkState", this));

        ChangeState(GetState("HostileIdleState"));
    }
}