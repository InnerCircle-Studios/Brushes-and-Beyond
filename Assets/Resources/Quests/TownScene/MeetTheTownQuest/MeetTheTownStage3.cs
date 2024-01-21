using System;
using System.Collections.Generic;

using UnityEngine;


public class MeetTheTownStage3 : QuestStage {
    [SerializeField] CharacterAttributes blacksmith;
    [SerializeField] CharacterAttributes brushy;
    private bool blockadeCleared = false;
    string blockadeName = "TownBridgeBlockade";

    WindowManager wm;

    private void OnEnable() {
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Repair the bridge");
        wm.SetQuestObjectives($"* Go to the bridge");
        SetupInitialDialogue();
    }

    private void SetupInitialDialogue() {
        Actor player = GameManager.Instance.GetPlayer();

        EventWrapper vanishevent = new();
        vanishevent.AddListener(() => InteractionEvents.HideObject(blockadeName));

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Blacksmith", new(new List<DialogueEntry>() {
                    new(blacksmith, "You have found them! Amazing!", DialogueActorMood.HAPPY),
                    new(blacksmith, "Now go quickly! Repair the bridge!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){})
            },
            { "BridgeBlockade", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "With the power of friendship I repair this bridge!", DialogueActorMood.CONFUSED),
                    new(brushy, "Together we can do anything!", DialogueActorMood.NEUTRAL),

                }, new List<DialogueAction>(){
                    new(2,vanishevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;

    }

    private void OnDisable() {
    }

    private void Start() {
        if (blockadeCleared) {
            AfterVanish();
        }
    }


    private void OnWallVanished(string name) {
        if (name == blockadeName) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().SetPaint(0);
            blockadeCleared = true;
            AfterVanish();
            CheckCompleted();
        }
    }

    private void AfterVanish() {
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Blacksmith", new(new List<DialogueEntry>() {
                    new(blacksmith, "Well done painter! Into the forest we go!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){})
            },
        });
        wm.SetQuestName("Repair the bridge");
        wm.SetQuestObjectives($"* Enter the forest");
    }




    private void CheckCompleted() {
        UpdateState();
        if (blockadeCleared) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(blockadeCleared));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        blockadeCleared = data.BlockadeCleared;
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public bool BlockadeCleared;

        public StupidJSONWrapper(bool blockadeCleared) {
            BlockadeCleared = blockadeCleared;
        }
    }
}