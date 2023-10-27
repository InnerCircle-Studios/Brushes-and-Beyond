public abstract class PlayerBaseState 
{
    protected PlayerStateMachine Ctx;
    protected PlayerStateFactory Factory;

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory){
        Ctx = currentContext;
        Factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubStates();
    protected void SwitchState(PlayerBaseState newState){
        //Current state exits state
        ExitState();

        //New state enters state
        newState.EnterState();
        // switch current state of context
        Ctx.CurrentState = newState;
    }



}
