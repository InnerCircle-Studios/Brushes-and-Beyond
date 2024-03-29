
using System;
using System.Collections.Generic;

using UnityEngine;

public class FirstPaintQuestStage2 : QuestStage {
    [SerializeField] CharacterAttributes brushy;

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
                    new(brushy, "You have enough paint! Go and fill in the blank spot over there", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){

                })
            },
            { "Blockade 1", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "What do I do now?", DialogueActorMood.CONFUSED),
                    new(GameManager.Instance.GetPlayer(), "Maybe if I tap it with my brush?", DialogueActorMood.NEUTRAL),
                    new(GameManager.Instance.GetPlayer(), "Woah!", DialogueActorMood.SCARED),
                    new(GameManager.Instance.GetPlayer(), "How did I do that?!", DialogueActorMood.CONFUSED),

                }, new List<DialogueAction>(){
                    new(2,vanishevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;
    }

    private void OnDisable() {
        // Reset dialogue back to default
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Well done! On you go brave painter!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){

                })
            },
            { "Blockade 1", null }
        });
        QuestEvents.OverrideBaseDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Well done! On you go brave painter!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){

                })
            }
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