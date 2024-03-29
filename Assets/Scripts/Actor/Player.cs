using System;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Player : Actor, ISaveable {

    public override void Start() {
        _PlayerStateMachine = new PlayerStateMachine(this);
        InteractionEvents.OnPaintBucketActivated += HandlePaintAdded;
    }

    public override void Update() {
        _PlayerStateMachine.GetCurrentState().UpdateState();
        if (!GetAttrubuteManager().IsAlive()) {
            OnDeath();
        }

        GameManager.Instance.GetWindowManager().UpdateHealthBar(_PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().CurrentHealth); // Replace with event
        GameManager.Instance.GetWindowManager().UpdateTextWindow("PaintIndicator", _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().PaintCount.ToString());

        HandleInteractions();
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, _AttributeManager.GetAttributes().AttackRange, "Enemy")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
            hits.Knockback(hits.GetRigidBody().position - GetRigidBody().position);

            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));

        }
    }

    public override void HandleRangedAttack() {

    }

    private void HandlePaintAdded(int amount) {
        int newAmount = _AttributeManager.GetAttributes().PaintCount + amount;
        if (!(newAmount > 3)) {

            _AttributeManager.SetPaint(newAmount);
        }
    }

    private void HandleInteractions() {
        Interactable toBeInteracted = GetClosestInteractable();

        // Disable indicators for every interactable not currently the closest in range. 
        FindObjectsOfType<Interactable>().Where(o => o != toBeInteracted && o.isActiveAndEnabled).ToList().ForEach(o => o.DeactivateIndicator());

        bool inHideState = _PlayerStateMachine.GetCurrentState().GetName() == "PlayerDialogueState";

        if (toBeInteracted != null && !inHideState) {
            toBeInteracted.ActivateIndicator();
        }
        else if (toBeInteracted != null && inHideState) {
            toBeInteracted.DeactivateIndicator();
        }
    }

    public Interactable GetClosestInteractable() {
        Vector2 currentPosition = _PlayerStateMachine.GetActor().transform.position;

        Interactable closestInteractable = null;
        float smallestDistance = 100f;

        // Find the closest interactable in a circle arround the player.
        foreach (RaycastHit2D hit in Physics2D.CircleCastAll(currentPosition, 10, Vector2.zero)) {
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable)) {
                if (interactable.isActiveAndEnabled ) {
                    float distanceBetweenTargets = Vector2.Distance(currentPosition, interactable.gameObject.transform.position);
                    float interactionRange = interactable.GetInteractionRange();
                    if (distanceBetweenTargets < smallestDistance && distanceBetweenTargets <= interactionRange) {
                        closestInteractable = interactable;
                        smallestDistance = distanceBetweenTargets;
                    }
                }
            }
        }
        return closestInteractable;
    }

    public void OnDeath() {
        EventBus.TriggerEvent(EventBusEvents.EventName.DEATH_EVENT, true);
    }

    public void LoadData(GameData data) {
        if (data.PlayerData.PlayerPosition != Vector3.zero && data.PlayerData.SceneName == SceneManager.GetActiveScene().name) {
            transform.position = data.PlayerData.PlayerPosition;
        }
        if (data.PlayerData.PlayerAttributes.Type == ActorType.PLAYER) {
            GetAttrubuteManager().Setattributes(data.PlayerData.PlayerAttributes);
        }
    }

    public void SaveData(GameData data) {
        data.PlayerData.SceneName = SceneManager.GetActiveScene().name;
        data.PlayerData.PlayerPosition = transform.position;
        data.PlayerData.PlayerAttributes = GetAttrubuteManager().GetAttributes();
        data.PlayerData.SceneName = SceneManager.GetActiveScene().name;
    }

    private PlayerStateMachine _PlayerStateMachine;
}