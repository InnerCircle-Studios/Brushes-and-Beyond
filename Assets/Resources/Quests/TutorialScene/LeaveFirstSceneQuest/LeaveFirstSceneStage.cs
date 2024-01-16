using System;
using System.Collections.Generic;

using UnityEngine;

//TODO start from inky's quest, enemy kill count? || paint bucket count?

public class LeaveFirstSceneStage : QuestStage {
    WindowManager wm;

    private void OnEnable() {

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Explore");
        wm.SetQuestObjectives($"* Find a way to leave");


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Barrier", new(new List<DialogueEntry>() {

                }, new List<DialogueAction>(){

                })
            }
        });
    }

    private void OnDisable() {

    }

    private void Start() {

    }






    private void CheckCompleted() {
        UpdateState();

    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(
            new bool[] { }
        ));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        bool[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
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