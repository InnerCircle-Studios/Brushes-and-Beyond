public class PaintTubeStateMachine : StateMachine
{
    public PaintTubeStateMachine(Actor actor) : base(actor)
    {
        AddState(new PaintTubeIdleState("PaintTubeIdleState", this));
        AddState(new PaintTubeWalkState("PaintTubeWalkState", this));
        AddState(new PaintTubeSpawnState("PaintTubeSpawnState", this));
        AddState(new PaintTubeDeathState("PaintTubeDeathState", this));

        ChangeState(GetState("PaintTubeIdleState"));
    }


    


    public BoolWrapper _isInRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    





}