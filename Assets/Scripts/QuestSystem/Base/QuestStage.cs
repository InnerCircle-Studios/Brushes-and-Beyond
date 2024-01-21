using UnityEngine;

public abstract class QuestStage : MonoBehaviour {
    private bool isDone = false;
    private string questId;
    private int stageIndex;
    public void Init(string questId, int stageIndex, string questStageState) {
        this.questId = questId;
        this.stageIndex = stageIndex;
        if (questStageState != null && questStageState != "") {
            SetQuestStageState(questStageState);
        }
    }

    protected void FinishStage() {
        if (!isDone) {
            isDone = true;
            QuestEvents.AdvanceQuest(questId);
            Destroy(gameObject);
        }
    }

    protected void ChangeState(string newState) {
        QuestEvents.ChangeQuestStageState(questId, stageIndex, new QuestStageState(newState));
    }

    protected abstract void SetQuestStageState(string state);
}