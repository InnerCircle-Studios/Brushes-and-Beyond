using UnityEngine;

public class Hostile : Actor {
    public override void Start() {
        _HostileStateMachine = new HostileStateMachine(this, _Player);
    }

    public override void Update() {
        _HostileStateMachine.GetCurrentState().UpdateState();
        if (!GetAttrubuteManager().IsAlive()) {
            OnDeath();
        }
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, 1.5f, "Player")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);

            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));

        }
    }

    public override void HandleRangedAttack() {

    }

    public Player GetPlayer()
    {
        return _Player;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private HostileStateMachine _HostileStateMachine;

    [SerializeField]private Player _Player;
}