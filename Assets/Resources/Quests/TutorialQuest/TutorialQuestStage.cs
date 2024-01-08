
using System;
using System.Collections.Generic;

using UnityEngine;

public class TutorialQuestStage : QuestStage {
    private bool hasMoved = false;
    private bool hasAttacked = false;
    private bool hasSprinted = false;

    private void OnEnable() {
        // Load dialogue for character during quest 
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetBrushy(), "Welcome to the tutorial!", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "This is a tutorial quest, it will teach you the basics of the game.",DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "You can move around with WASD or the arrow keys.",DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "Attack enemies by using the space bar", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "Movement speed can be increased by using the shift key to sprint.", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "Interact with objects by pressing E",DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "If you need a break, you can open the pause menu with the escape key.",DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "Good luck!",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>())
            }
        });

        EventBus.StartListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
    }

    private void OnDisable() {
        // Reset dialogue back to default
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", null }
        });

        EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);

        // Start the next quest
        QuestEvents.StartQuest("FirstPaintQuest");
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
            GameManager.Instance.GetPlayer().GetAttrubuteManager().GetAttributes().Level = 2;
            FinishStage();
        }
    }

    private void UpdateState() {

        string data = JsonUtility.ToJson(new StupidJSONWrapper(new bool[] { hasMoved, hasAttacked, hasSprinted }));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        hasMoved = data[0];
        hasAttacked = data[1];
        hasSprinted = data[2];
        // Debug.Log($"[ TutorialQuestStage ]: {data}");

        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public bool[] Values;
        public StupidJSONWrapper(bool[] values) {
            Values = values;
        }
    }



}