using UnityEngine;

public class Hostile : Actor {
    public override void Start() {
        _Player = FindAnyObjectByType<Player>();

        _HostileStateMachine = new HostileStateMachine(this, _Player, _isMelee, _colour.ToString());
    }

    public override void Update() {
        _HostileStateMachine._Colour = _colour.ToString();

        _HostileStateMachine.GetCurrentState().UpdateState();
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

    }

    public Player GetPlayer() {
        return _Player;
    }

    private void OnDeath() {
        _HostileStateMachine._isDead.Value = true;
    }

    private bool GetHostileType() {
        return _isMelee;
    }


    private HostileStateMachine _HostileStateMachine;

    private Player _Player;
    [SerializeField] private bool _isMelee = false;
    [SerializeField] private Colour _colour;


    public enum Colour {
        Red,
        Yellow,
        Blue,
    }
}