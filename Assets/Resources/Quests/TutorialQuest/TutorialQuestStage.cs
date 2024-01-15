
using System;
using System.Collections.Generic;

using UnityEngine;

public class TutorialQuestStage : QuestStage {
    [SerializeField] CharacterAttributes brushy;
    private bool hasMoved = false;
    private bool hasAttacked = false;
    private bool hasSprinted = false;

    private bool dialogueFinished = false;
    private bool inDialogue = false;

    private void OnEnable() {
        // Load dialogue for character during quest 
        EventWrapper dialogueEvent = new();
        dialogueEvent.AddListener(() => { dialogueFinished = true; OnQuestShow(); UpdateDialogue(); });


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Welcome to the tutorial!", DialogueActorMood.HAPPY),
                    new(brushy, "This is a tutorial quest, it will teach you the basics of the game.",DialogueActorMood.HAPPY),
                    new(brushy, "You can move around with WASD.",DialogueActorMood.HAPPY),
                    new(brushy, "Attack enemies by using the space bar", DialogueActorMood.HAPPY),
                    new(brushy, "Movement speed can be increased by using the shift key to sprint.", DialogueActorMood.HAPPY),
                    new(brushy, "Interact with objects by pressing <sprite=\"Ekey\" index=0>",DialogueActorMood.HAPPY),
                    new(brushy, "If you need a break, you can open the pause menu with the escape key.",DialogueActorMood.HAPPY),
                    new(brushy, "Good luck!",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(99,dialogueEvent,true)
                })
            },
        });

        EventBus.StartListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        EventBus.StartListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEnter);

    }

    private void Start() {
        if (dialogueFinished) {
            OnQuestShow();
        }
    }

    private void OnDisable() {
        // Reset dialogue back to default
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", null }
        });
        // Override base dialogue to avoid repeating the tutorial start sequence when a quest resets Brushy's dialogue
        QuestEvents.OverrideBaseDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Hi vin!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                })
            }
        });

        EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        EventBus.StopListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEnter);
        // Start the next quest
        QuestEvents.StartQuest("FirstPaintQuest");
    }

    private void UpdateDialogue() {
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Don't you remember? You can move around with WASD and attack enemies by using the space bar",DialogueActorMood.HAPPY),
                    new(brushy, "Movement speed can be increased by using the shift key to sprint.", DialogueActorMood.HAPPY),
                    new(brushy, "If you need a break, you can open the pause menu with the escape key.",DialogueActorMood.HAPPY),
                    new(brushy, "Good luck!",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                })
            },
        });
    }


    private void OnQuestShow() {
        WindowManager wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("TutorialQuest");
        wm.SetQuestObjectives($"* Move with WASD : {hasMoved}\n* Attack with Space : {hasAttacked}\n* Sprint with Shift : {hasSprinted}");
        UpdateState();
    }

    private void OnDialogueEnter(bool value) {
        inDialogue = value;
    }


    private void OnMove(Vector2 a) {
        if (dialogueFinished && !inDialogue) {
            hasMoved = true;
            CheckCompleted();
            EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMove);
        }

    }
    private void OnAttack(bool b) {
        if (dialogueFinished && !inDialogue) {
            hasAttacked = true;
            CheckCompleted();
            EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        }
    }
    private void OnSprint(bool b) {
        if (dialogueFinished && !inDialogue) {
            hasSprinted = true;
            CheckCompleted();
            EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        }
    }


    private void CheckCompleted() {
        UpdateState();
        OnQuestShow();
        if (hasMoved && hasAttacked && hasSprinted) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().GetAttributes().Level = 2;
            GameManager.Instance.GetWindowManager().ClearQuest();
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(new bool[] { hasMoved, hasAttacked, hasSprinted, dialogueFinished }));
        ChangeState(data);
    }



    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        if (data.Length == 4) {
            hasMoved = data[0];
            hasAttacked = data[1];
            hasSprinted = data[2];
            dialogueFinished = data[3];
        }

    }

    [Serializable]
    public class StupidJSONWrapper {
        public bool[] Values;
        public StupidJSONWrapper(bool[] values) {
            Values = values;
        }
    }



}