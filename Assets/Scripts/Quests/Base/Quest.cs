using UnityEngine;

public abstract class Quest {

    protected QuestChannel questChannel;
    private int id;
    private string questName;
    private QuestState state;
    private int requirement;
    private int rewards;
    private int progress;


    
    protected void EnableQuest() {
        questChannel.QuestActivatedEvent += QuestActiveEvent;
        questChannel.QuestCompletedEvent += QuestCompletedEvent;
        questChannel.QuestUpdate += QuestUpdatedEvent;

        if (state == QuestState.ACTIVE) {
            QuestActive();
        }
    }
    protected void DisableQuest() {
        questChannel.QuestActivatedEvent -= QuestActiveEvent;
        questChannel.QuestCompletedEvent -= QuestCompletedEvent;
    }
     protected void CompleteQuest() {
        questChannel.CompleteQuest(this);
    }

    protected abstract void QuestActive();
    protected abstract void QuestCompleted();
    protected abstract void QuestUpdated();



    private void QuestActiveEvent(Quest activeQuest) {
        if (activeQuest.id == id) {
            state = QuestState.ACTIVE;
            QuestActive();
        }
    }
    private void QuestCompletedEvent(Quest completedQuest) {
        if (completedQuest.id == id) {
            state = QuestState.COMPLETED;
            QuestCompleted();
        }
    }
    private void QuestUpdatedEvent(Quest updatedQuest, int progress){
        if(updatedQuest.id == id){
            this.progress = progress;
            QuestUpdated();
        }
    }


   



    





}