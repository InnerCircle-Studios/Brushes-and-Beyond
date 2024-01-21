using System;

[Serializable]
public class QuestData {
    public QuestState State;
    public int QuestStageIndex;
    public QuestStageState[] StageStates;

    public QuestData(QuestState state, int questStageIndex, QuestStageState[] stageStates) {
        State = state;
        QuestStageIndex = questStageIndex;
        StageStates = stageStates;
    }

}