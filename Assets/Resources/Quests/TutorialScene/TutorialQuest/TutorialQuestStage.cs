
using System;
using System.Collections.Generic;

using UnityEngine;

public class TutorialQuestStage : QuestStage {
    [SerializeField] CharacterAttributes brushy;
    private bool hasAttacked = false;
    private bool hasSprinted = false;

    private bool dialogueFinished = false;
    private bool inDialogue = false;
    private string playerState = "";

    private void OnEnable() {
        // Load dialogue for character during quest 
        EventWrapper dialogueEvent = new();
        dialogueEvent.AddListener(() => { dialogueFinished = true; OnQuestShow(); UpdateDialogue(); });
        Player player = GameManager.Instance.GetPlayer();

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(player, "Who are you and why am I here?!", DialogueActorMood.CONFUSED),

                    new(brushy, "Long story short, the paintings started to riot. You never finish any of them! ", DialogueActorMood.NEUTRAL),
                    new(brushy, "As for me.. I am Brushy! You know, the brush you painted with before you got here? Yep, That's me! ", DialogueActorMood.NEUTRAL),

                    new(player, "Okay Brushy, I promise I'll finish my paintings. … but can I get out of here now?", DialogueActorMood.SAD),
                    new(brushy, "Nope you have to finish the painting from inside out, but don't worry I will come with you!", DialogueActorMood.NEUTRAL),

                    new(brushy, "I also have some tips for you!", DialogueActorMood.HAPPY),
                    new(brushy, "You can use the shift key to sprint and increase your movement speed.", DialogueActorMood.NEUTRAL),
                    new(brushy, "To defend yourself, press space and swing your brush!", DialogueActorMood.NEUTRAL),
                    new(brushy, "If you need a break, you can open the pause menu with the escape key.", DialogueActorMood.NEUTRAL),
                    new(brushy, "Good luck!",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(99,dialogueEvent,true)
                })
            },
        });

        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        EventBus.StartListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEnter);
        EventBus.StartListening<string>(EventBusEvents.EventName.SWITCH_STATE_EVENT, OnStateChange);
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

        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        EventBus.StopListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEnter);
        EventBus.StopListening<string>(EventBusEvents.EventName.SWITCH_STATE_EVENT, OnStateChange);

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
        wm.SetQuestObjectives($"* Attack with Space : {hasAttacked}\n* Sprint with Shift : {hasSprinted}");
        UpdateState();
    }

    private void OnDialogueEnter(bool value) {
        inDialogue = value;
    }
    private void OnStateChange(string state) {
        playerState = state;
    }

    private void OnAttack(bool b) {
        if (dialogueFinished && !inDialogue) {
            hasAttacked = true;
            CheckCompleted();
            EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttack);
        }
    }
    private void OnSprint(bool b) {
        if (dialogueFinished && !inDialogue && (playerState == "PlayerWalkState" || playerState == "PlayerSprintState")) {
            hasSprinted = true;
            CheckCompleted();
            EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnSprint);
        }
    }


    private void CheckCompleted() {
        UpdateState();
        OnQuestShow();
        if (hasAttacked && hasSprinted) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().GetAttributes().Level = 2;
            GameManager.Instance.GetWindowManager().ClearQuest();
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(new bool[] { hasAttacked, hasSprinted, dialogueFinished }));
        ChangeState(data);
    }



    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        if (data.Length == 4) {
            hasAttacked = data[0];
            hasSprinted = data[1];
            dialogueFinished = data[2];
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