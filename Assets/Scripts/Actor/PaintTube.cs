using UnityEngine;

public class PaintTube : Actor {
    public override void Start() {
        _PaintTubeStateMachine = new PaintTubeStateMachine(this, _Player, _colour.ToString());
    }

    public override void Update() {
        _PaintTubeStateMachine._colour = _colour.ToString();
        _PaintTubeStateMachine.GetCurrentState().UpdateState();
        if (!GetAttrubuteManager().IsAlive()) {
            OnDeath();
        }
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, _AttributeManager.GetAttributes().AttackRange, "Player")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);

            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));
        }
    }

    public override void HandleRangedAttack() {
        Instantiate(_paintTubeProjectile, transform.position, Quaternion.identity);
    }

    public Player GetPlayer() {
        return _Player;
    }

    private void OnDeath() {
        _PaintTubeStateMachine._isDead.Value = true;
    }

    private PaintTubeStateMachine _PaintTubeStateMachine;

    [SerializeField] private Player _Player;
    [SerializeField] private GameObject _paintTubeProjectile;
    [SerializeField] private Colour _colour;

    public enum Colour {
        Red,
        Yellow,
        Blue,
        Rainbow,
    }
}