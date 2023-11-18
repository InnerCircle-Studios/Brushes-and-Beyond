using System;

public interface IQuest {
    public void EnableQuest();
    public void DisableQuest();
    public void QuestActiveEvent(Quest activeQuest);
    public void QuestActive();
    public void QuestCompletedEvent(Quest completedQuest);
    public void QuestCompleted();
    public void CompleteQuest();
}