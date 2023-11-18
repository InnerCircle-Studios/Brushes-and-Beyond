using System;

public class QuestChannel {
    private Action<Quest> questCompletedEvent;
    private Action<Quest> questActivatedEvent;

    public void CompleteQuest(Quest completedQuest) {
        questCompletedEvent.Invoke(completedQuest);
    }

    public void AssignQuest(Quest questToAssign) {
        questActivatedEvent.Invoke(questToAssign);
    }
}