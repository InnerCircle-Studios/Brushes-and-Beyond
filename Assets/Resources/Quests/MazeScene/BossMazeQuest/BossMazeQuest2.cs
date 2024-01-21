using System;
using System.Collections.Generic;

using UnityEngine;

//TODO start from inky's quest, enemy kill count? || paint bucket count?

public class BossMazeQuest2 : QuestStage {
    private string blockadeName = "BossBlockade";
    private bool hasVanished = false;
    WindowManager wm;

    private void OnEnable() {

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("The Final Boss");
        wm.SetQuestObjectives($"* Get that Ink man!");

        EventWrapper vanishevent = new();
        vanishevent.AddListener(() => InteractionEvents.HideObject(blockadeName));

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Blockade 4 maze", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "We're so close", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "Lets go get him!", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(99,vanishevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;

    }

    private void OnDisable() {
        QuestEvents.StartQuest("SecondMazeQuest");
    }

    private void Start() {
        if (hasVanished) {
            UpdateQuestWindow();
        }
    }

    private void OnWallVanished(string name) {
        if (name == blockadeName) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().SetPaint(0);
            hasVanished = true;
            UpdateQuestWindow();
            CheckCompleted();
        }
    }

    private void UpdateQuestWindow() {
        wm.SetQuestObjectives($"* Continue onwards");
    }


    private void CheckCompleted() {
        UpdateState();
        if (hasVanished) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(hasVanished));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        hasVanished = data.HasVanished;
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public bool HasVanished;
        public StupidJSONWrapper(bool hasVanished) {
            HasVanished = hasVanished;
        }
    }
}