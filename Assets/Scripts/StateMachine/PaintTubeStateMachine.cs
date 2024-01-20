using UnityEngine;

public class PaintTubeStateMachine : StateMachine
{
    public PaintTubeStateMachine(Actor actor, Player player, string colour) : base(actor)
    {
        AddState(new PaintTubeIdleState("PaintTubeIdleState", this));
        AddState(new PaintTubeWalkState("PaintTubeWalkState", this));
        AddState(new PaintTubeSpawnState("PaintTubeSpawnState", this));
        AddState(new PaintTubeDeathState("PaintTubeDeathState", this));

        ChangeState(GetState("PaintTubeWalkState"));

        InitSwitchCases();

        _colour = colour;

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
    public void PlayRandomDeathSound() {
        int random = Random.Range(0, 3);

        switch (random) {
            case 0:
                AudioManager.instance.PlaySfx("PaintBallHit1");
                break;
            case 1:
                AudioManager.instance.PlaySfx("PaintBallHit2");
                break;

        }
    }

    public Vector2 _currentMovement = new Vector2();
    public float _hostileRange = 10f;
    public string _colour = "Red";

    public BoolWrapper _isInRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isIdle { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isSpawning { get; set; } = new BoolWrapper(false);

    private PaintTube _PaintTube;
}