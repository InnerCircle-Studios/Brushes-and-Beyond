using System.Collections;

using UnityEngine;

public class Player : Actor {
    public override void Start() {
        _PlayerStateMachine = new PlayerStateMachine(this);
    }

    public override void Update() {
        _PlayerStateMachine.GetCurrentState().UpdateState();
        if (!GetAttrubuteManager().IsAlive()) {
            OnDeath();
        }
        _PlayerStateMachine.GetActor().GetWindowManager().UpdateTextWindow("HealthIndicator", _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().CurrentHealth.ToString());
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, 1.5f, "Enemy")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
            hits.Knockback(hits.GetRigidBody().position - GetRigidBody().position);

            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));
            
        }
    }



    public override void HandleRangedAttack() {

    }

    public void OnDeath() {
        EventBus.TriggerEvent(EventBusEvents.EventName.DEATH_EVENT, true);
    }

    private PlayerStateMachine _PlayerStateMachine;
}