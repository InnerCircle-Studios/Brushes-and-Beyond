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
    #Queststate
        - Changed Cancelled > FAILED
        - Put everything in CAPS
    #Quest
        - Made the class abstract
        - Changed uniqueId > id
        - Changed name > questName
    #IQuest
    #QuestManager
        - QuestCompletedEvent needs no () brackets
*/