using System;
using System.Collections.Generic;

using UnityEngine;

//TODO start from inky's quest, enemy kill count? || paint bucket count?

public class BossMazeQuest : QuestStage {
    private int paintCounter = 0;

    WindowManager wm;

    private void OnEnable() {
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Painting");
        wm.SetQuestObjectives($"* Kill Inky's minions and collect the buckets");
        InteractionEvents.OnPaintBucketActivated += OnPaintBucketActivated;
    }

    private void OnDisable() {

    }

    private void Start() {

    }

    private void OnPaintBucketActivated(int amount) {
        paintCounter++;
        wm.SetQuestObjectives($"* Go get that ink man!");
        CheckCompleted();
    }


    private void CheckCompleted() {
        UpdateState();
        if (paintCounter >= 3) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(paintCounter));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        paintCounter = data.PaintCounter;
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public int PaintCounter;
        public StupidJSONWrapper(int paintCounter) {
            PaintCounter = paintCounter;
        }
    }



}