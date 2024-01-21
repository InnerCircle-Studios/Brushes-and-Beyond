
using UnityEngine;

public class Quest {
    public QuestInfo Info;
    public QuestState State;

    private int currentStage;
    private QuestStageState[] stageStates;

    public Quest(QuestInfo info) {
        Info = info;
        State = QuestState.UNAVAILABLE;
        currentStage = 0;
        stageStates = new QuestStageState[Info.Stages.Length];
        for (int i = 0; i < stageStates.Length; i++) {
            stageStates[i] = new QuestStageState();
        }
    }

    public Quest(QuestInfo info, QuestState state, int currentStage, QuestStageState[] stageStates) {
        Info = info;
        State = state;
        this.currentStage = currentStage;
        this.stageStates = stageStates;
        if (this.stageStates.Length != this.Info.Stages.Length) {
            Logger.LogError("Quest","QuestStageState length does not match QuestInfo.Stages length for quest: " + Info.Id + " stage: " + currentStage + "");
        }
    }


    public void MoveToNextStage() {
        currentStage++;
    }

    public bool CurrentStageExists() {
        return currentStage < Info.Stages.Length;
    }

    public void InitCurrentQuestStep(Transform parent) {
        GameObject stagePrefab = GetCurrentStagePrefab();
        if (stagePrefab != null) {
            GameObject stage = Object.Instantiate(stagePrefab, parent);
            stage.GetComponent<QuestStage>().Init(Info.Id, currentStage, stageStates[currentStage].State);
        }
    }

    private GameObject GetCurrentStagePrefab() {
        if (CurrentStageExists()) {
            return Info.Stages[currentStage];
        }
        else {
            Logger.LogWarning("Quest", "No next questStage for quest: " + Info.Id + " stage: " + currentStage + "");
            return null;
        }
    }

    public void StoreQuestStageState(QuestStageState questStageState, int index) {
        if (index < stageStates.Length) {
            stageStates[index].State = questStageState.State;
        }
        else {
            Logger.LogError("Quest", "Stepindex out of range for quest: " + Info.Id + " stage: " + currentStage + "");
        }
    }

    public QuestData GetQuestData() {
        return new QuestData(State, currentStage, stageStates);
    }
}