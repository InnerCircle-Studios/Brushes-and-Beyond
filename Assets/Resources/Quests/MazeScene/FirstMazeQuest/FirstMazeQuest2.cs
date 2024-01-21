using System;
using System.Collections.Generic;

using UnityEngine;

//TODO start from inky's quest, enemy kill count? || paint bucket count?

public class FirstMazeQuest2 : QuestStage {
    private string blockadeName = "FMQblockade";
    private bool hasVanished = false;
    WindowManager wm;

    private void OnEnable() {

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Explore");
        wm.SetQuestObjectives($"* Find a way to leave");

        EventWrapper vanishevent = new();
        vanishevent.AddListener(() => InteractionEvents.HideObject(blockadeName));

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Blockade 1 maze", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "We've done this before!", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "I wonder where all that paint goes...", DialogueActorMood.CONFUSED),
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