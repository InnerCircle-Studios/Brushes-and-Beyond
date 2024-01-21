using System;
using System.Collections.Generic;

using UnityEngine;

//TODO start from inky's quest, enemy kill count? || paint bucket count?

public class ThirdMazeQuest2 : QuestStage {
    private string blockadeName = "TMQblockade";
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
            { "Blockade 3", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "This is the last one!", DialogueActorMood.SCARED),
                    new(GameManager.Instance.GetPlayer(), "Lets go get my paint back.", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(99,vanishevent ,true)
                })
            }
        });
        InteractionEvents.OnHideObject += OnWallVanished;

    }

    private void OnDisable() {
      //  QuestEvents.StartQuest("ThirdMazeQuest");
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