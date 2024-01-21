using System;
using System.Collections.Generic;

using UnityEngine;


public class LeaveFirstSceneStage : QuestStage {
    private int paintCounter = 0;

    WindowManager wm;

    private void OnEnable() {

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("The Ink man");
        wm.SetQuestObjectives($"* Meet Inky's friends");
        InteractionEvents.OnPaintBucketActivated += OnPaintBucketActivated;
    }

    private void OnDisable() {
        InteractionEvents.OnPaintBucketActivated -= OnPaintBucketActivated;
    }

    private void Start() {

    }

    private void OnPaintBucketActivated(int amount) {
        paintCounter++;
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