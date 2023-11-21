public class NpcStateMachine : StateMachine
{
    public NpcStateMachine(Actor actor) : base(actor)
    {
        AddState(new NpcDialogueState("NpcDialogueState", this));
        AddState(new NpcIdleState("NpcIdleState", this));
        AddState(new NpcWalkState("NpcWalkState", this));

        ChangeState(GetState("NpcIdleState"));
    }
}