using System;

public class QuestChannel {

    public delegate void QuestEvent(Quest quest);
    public event QuestEvent QuestCompletedEvent;
    public event QuestEvent QuestActivatedEvent;

    public delegate void QuestUpdateEvent(Quest quest, int progress);
    public event QuestUpdateEvent QuestUpdate;
    public void CompleteQuest(Quest completedQuest) {
        QuestCompletedEvent.Invoke(completedQuest);
    }

    public void AssignQuest(Quest questToAssign) {
        QuestActivatedEvent.Invoke(questToAssign);
    }

    public void UpdateQuestProgress(Quest questToUpdate, int progress){
        QuestUpdate.Invoke(questToUpdate,progress);
    }
}