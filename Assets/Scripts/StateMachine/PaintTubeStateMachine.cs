using UnityEngine;

public class PaintTubeStateMachine : StateMachine
{
    public PaintTubeStateMachine(Actor actor, Player player) : base(actor)
    {
        AddState(new PaintTubeIdleState("PaintTubeIdleState", this));
        AddState(new PaintTubeWalkState("PaintTubeWalkState", this));
        AddState(new PaintTubeSpawnState("PaintTubeSpawnState", this));
        AddState(new PaintTubeDeathState("PaintTubeDeathState", this));

        ChangeState(GetState("PaintTubeIdleState"));

        InitSwitchCases();

        _PaintTube = GetActor() as PaintTube;
    }

    public void CheckPlayerInRange()
    {
        float distance = Vector2.Distance(GetActor().transform.position, _PaintTube.GetPlayer().transform.position);

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
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isIdle { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isSpawning { get; set; } = new BoolWrapper(false);

    private PaintTube _PaintTube;
}