
using System;
using System.Collections.Generic;

using UnityEngine;

public class FirstPaintQuestStage2 : QuestStage {
    string blockadeName = "FirstPaintQuestBlockade";
    private bool hasVanished = false;
    WindowManager wm;

    private void OnEnable() {
        // Load dialogue for character during quest 
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Painting");
        wm.SetQuestObjectives($"* Fill in the blank spot");

        EventWrapper vanishevent = new();
        vanishevent.AddListener(() => InteractionEvents.HideObject(blockadeName));


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetBrushy(), "You have enough paint! Go fill in the blank spot over there", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){

                })
            },
            { "Blockade 1", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "...", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(0,vanishevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;
    }

    private void OnDisable() {
        // Reset dialogue back to default
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetBrushy(), "Well done! On you go brave painter!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){

                })
            },
            { "Blockade 1", null }
        });
        QuestEvents.StartQuest("InkyStartQuest");
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
            wm.ClearQuest();
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