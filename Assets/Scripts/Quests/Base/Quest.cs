using UnityEngine;

public abstract class Quest : IQuest {
    [SerializeField] private int id;
    [SerializeField] private string questName;
    [SerializeField] private QuestState state;
    [SerializeField] private int requirement;
    [SerializeField] private int rewards;
    [SerializeField] private int progress;

    public Quest(int id, string questName, QuestState state, int requirement, int rewards, int progress) {
        this.id = id;
        this.questName = questName;
        this.state = state;
        this.requirement = requirement;
        this.rewards = rewards;
        this.progress = progress;
    }

    public void EnableQuest() {
        state = QuestState.ACTIVE;
    }
    public void DisableQuest() {
        state = QuestState.UNLOCKED;
    }

    public void QuestActiveEvent(Quest activeQuest) {

    }

    public abstract void QuestActive();



    public void QuestCompletedEvent(Quest completedQuest) {
    }

    public abstract void QuestCompleted();

    public void CompleteQuest() {
    }

}