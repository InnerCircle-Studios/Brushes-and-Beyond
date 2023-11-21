using System;

[Serializable]
public enum QuestState {
    PENDING,
    UNLOCKED,
    ACTIVE,
    COMPLETED,
    FAILED
}

//TODO:
// Have fun Noah
/*
    Just do the UML again, it looks nothing like the start
    #Queststate
        - Changed Cancelled > FAILED
        - Put everything in CAPS
    #Quest
        - Made the class abstract
        - Changed uniqueId > id
        - Changed name > questName
        - Added quest update methods: - QuestUpdatedEvent(Quest updatedQuest, int progress) : void, # QuestUpdated() : void
        - Made the 
    #IQuest
        Removed completly becouse unneccesary
    #QuestManager
        - QuestCompletedEvent needs no () brackets
    # QuestChannel
        - Added update event
        - Made events public
*/