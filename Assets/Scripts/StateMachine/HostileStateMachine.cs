using UnityEngine;

public class HostileStateMachine : StateMachine {
    public HostileStateMachine(Actor actor, Player player, bool isMelee, string colour) : base(actor) {
        AddState(new HostileSpawnState("HostileSpawnState", this));
        AddState(new HostileIdleState("HostileIdleState", this));
        AddState(new HostileAttackState("HostileAttackState", this));
        AddState(new HostileDeathState("HostileDeathState", this));
        AddState(new HostileWalkState("HostileWalkState", this));

        ChangeState(GetState("HostileSpawnState"));

        InitSwitchCases();

        _Hostile = GetActor() as Hostile;

        switch (colour) {
            case "Red":
                _Colour = "Red";
                _isMelee.Value = isMelee;
                break;

            case "Yellow":
                _Colour = "Yellow";
                _isMelee.Value = isMelee;
                _attackRange = 5f;
                break;

            case "Blue":
                _Colour = "Blue";
                _isMelee.Value = isMelee;
                break;
        }
    }


    public void CheckPlayerInRange() {
        float distance = Vector2.Distance(GetActor().transform.position, _Hostile.GetPlayer().transform.position);

        if (distance < _hostileRange) {
            _isInRange.Value = true;
        }
        else {
            _isInRange.Value = false;
        }
    }

    public void CheckPlayerInAttackRange() {
        float distance = Vector2.Distance(GetActor().transform.position, _Hostile.GetPlayer().transform.position);

        if (distance < _attackRange) {
            _isInAttackRange.Value = true;
        }
        else {
            _isInAttackRange.Value = false;
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
    public float _attackRange = 1.5f;
    public string _Colour = "Red";

    public BoolWrapper _isSpawned { get; set; } = new BoolWrapper(true);
    public BoolWrapper _isInRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isInAttackRange { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isMelee { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isDead { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isIdle { get; set; } = new BoolWrapper(false);
    public BoolWrapper _isShooting { get; set; } = new BoolWrapper(false);

    public Hostile _Hostile;
}