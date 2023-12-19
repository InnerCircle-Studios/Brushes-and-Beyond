using System.Linq;

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

        HandleInteractions();
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, 1.5f, "Enemy")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
            hits.Knockback(hits.GetRigidBody().position - GetRigidBody().position);

            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));

        }
    }

    private void HandleInteractions() {
        Interactable toBeInteracted = GetClosestInteractable();

        // Disable indicators for every interactable not currently the closest in range. 
        FindObjectsOfType<Interactable>().Where(o => o != toBeInteracted).ToList().ForEach(o => o.DeactivateIndicator());

        if (toBeInteracted != null) {
            toBeInteracted.ActivateIndicator();
        }
    }

    public Interactable GetClosestInteractable() {
        float interactionRange = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().InteractionRange;

        Vector2 currentPosition = _PlayerStateMachine.GetActor().transform.position;

        Interactable closestInteractable = null;
        float smallestDistance = 100f;

        // Find the closest interactable in a circle arround the player.
        foreach (RaycastHit2D hit in Physics2D.CircleCastAll(currentPosition, interactionRange, Vector2.zero)) {
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable)) {

                float distanceBetweenTargets = Vector2.Distance(currentPosition, interactable.gameObject.transform.position);
                if (distanceBetweenTargets < smallestDistance && distanceBetweenTargets <= interactionRange) {
                    closestInteractable = interactable;
                    smallestDistance = distanceBetweenTargets;
                }
            }
        }
        return closestInteractable;
    }



    public override void HandleRangedAttack() {

    }

    public void OnDeath() {
        EventBus.TriggerEvent(EventBusEvents.EventName.DEATH_EVENT, true);
    }

    private PlayerStateMachine _PlayerStateMachine;
}