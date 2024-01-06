
using System.Collections.Generic;

using UnityEngine;

public class TutorialQuestStage : QuestStage {
    private bool hasMoved = false;
    private bool hasAttacked = false;
    private bool hasSprinted = false;

    private void OnEnable() {
        EventBus.StartListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);


    }

    private void OnDisable() {
        EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);

    }

    private void OnMove(Vector2 a) {
        hasMoved = true;
        CheckCompleted();
        EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);

    }
    private void OnAttack(bool b) {
        hasAttacked = true;
        CheckCompleted();
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);

    }
    private void OnSprint(bool b) {
        hasSprinted = true;
        CheckCompleted();
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
    }

    private void CheckCompleted() {
        UpdateState();
        if (hasMoved && hasAttacked && hasSprinted) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string newState = hasMoved.ToString();
        ChangeState(newState);
    }

    protected override void SetQuestStageState(string state) {
        bool.TryParse(state, out hasMoved);
        UpdateState();
    }

}