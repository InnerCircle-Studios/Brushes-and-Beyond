
using System;
using System.Collections.Generic;

using UnityEngine;

public class InkyStartQuestStage2 : QuestStage {
    string blockadeName = "InkyStartQuestBlockade";
    private bool hasVanished = false;
    WindowManager wm;

    private void OnEnable() {
        // Load dialogue for character during quest 

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        EventWrapper vanishevent = new();
        vanishevent.AddListener(() => InteractionEvents.HideObject(blockadeName));

        EventWrapper UIevent = new();
        UIevent.AddListener(() => wm.SetQuestObjectives($"* Meet Inky's friends"));


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Inky", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetInky(), "Are you doing it right?", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){})
            },
            { "Blockade 2", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "That weird guy gave me some paint, let's see if that trick works a second time.", DialogueActorMood.NEUTRAL),
                    new(GameManager.Instance.GetPlayer(), "...", DialogueActorMood.NEUTRAL),
                    new(GameManager.Instance.GetPlayer(), "Nice! Now let's make some friends!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(1,vanishevent ,true),
                    new(1,UIevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;
    }


    private void OnDisable() {
        // Reset dialogue back to default
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Inky", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetInky(), "Yay you did it!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){})
            },
            { "Blockade 2", null }
        });

    }

    private void OnWallVanished(string name) {
        if (name == blockadeName) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().SetPaint(0);
            hasVanished = true;
            CheckCompleted();
        }
    }


    private void CheckCompleted() {
        UpdateState();
        if (hasVanished) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().GetAttributes().Level = 3;
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(new bool[] { hasVanished }));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        hasVanished = data[0];
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