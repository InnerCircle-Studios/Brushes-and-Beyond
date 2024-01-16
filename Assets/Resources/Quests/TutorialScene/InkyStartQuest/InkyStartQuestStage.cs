
using System;
using System.Collections.Generic;

using UnityEngine;

public class InkyStartQuestStage : QuestStage {
    [SerializeField] CharacterAttributes inky;

    bool isDoneTalking = false;
    WindowManager wm;

    private void OnEnable() {
        // Load dialogue for character during quest 
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("The Ink man");
        wm.SetQuestObjectives($"* Find Inky");

        EventWrapper paintevent = new();
        paintevent.AddListener(() => InkyGivePaint());
        EventWrapper uiEvent = new();
        uiEvent.AddListener(() => wm.SetQuestObjectives($"* Inspect the blank spot"));

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Inky", new(new List<DialogueEntry>() {
                    new(inky, "Hi Vin!", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "Who are you and how do you know my name?!", DialogueActorMood.NEUTRAL),
                    new(inky, "I am Inky, how I know your name is not important.", DialogueActorMood.NEUTRAL),
                    new(GameManager.Instance.GetPlayer(), "But...", DialogueActorMood.SAD),
                    new(inky, "You know how to paint right? I saw you fill in the blank space there.",DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "Well, yes but...", DialogueActorMood.NEUTRAL),
                    new(inky, "Good! Because here is another one of those. This one sits between me and my friends over there.",DialogueActorMood.ANGRY),
                    new(inky, "Here, I got some paint for you. Try and see if you can clear the way and meet my friends!",DialogueActorMood.HAPPY),

                }, new List<DialogueAction>(){
                    new(99,paintevent ,true), // 99 always triggers after last dialogue entry
                    new(99, uiEvent,true)
                })
            },
            { "Blockade 2", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "This spot looks unfinished...", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){})
            }
        });
    }


    private void OnDisable() {
        // Reset dialogue back to default

    }




    private void InkyGivePaint() {
        GameManager.Instance.GetPlayer().GetAttrubuteManager().SetPaint(3);
        isDoneTalking = true;
        CheckCompleted();
    }


    private void CheckCompleted() {
        UpdateState();
        if (isDoneTalking) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().GetAttributes().Level = 3;
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(new bool[] { isDoneTalking }));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        isDoneTalking = data[0];
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