using System;
using System.Collections.Generic;

using UnityEngine;

public class QuestManager : MonoBehaviour {
    private List<BaseQuest> quests = new();

    private BaseQuest activeQuest;

    public void Start() {
        quests.Add(new TutorialQuest());
        
    }

    public void SetQuestStage(int stage) {
        activeQuest.SetStage(stage);
    }
}