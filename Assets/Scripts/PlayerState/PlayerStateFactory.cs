public class PlayerStateFactory
{
    PlayerStateMachine Context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        Context = currentContext;
    }

    public PlayerBaseState Walk() {
        return new PlayerWalkState(Context, this);
    }
    public PlayerBaseState Run() {
        return new PlayerRunState(Context, this);
    }
    public PlayerBaseState Jump() {
        return new PlayerAttackState(Context, this);
    }
    public PlayerBaseState Idle() {
        return new PlayerIdleState(Context, this);
    }

}
