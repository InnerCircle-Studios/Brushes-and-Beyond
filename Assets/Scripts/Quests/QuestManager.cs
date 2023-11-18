using System;
using System.Collections.Generic;

public class QuestManager
{
    private List<Quest> activeQuests;
    private List<Quest> completedQuests;
    private List<Quest> availableQuests;
    private QuestChannel questChannel;

    public void AddQuest(Quest quest)
    {
        availableQuests.Add(quest);
    }
    public void CompleteQuest(Quest quest)
    {
        availableQuests.Remove(quest);
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        questChannel.CompleteQuest(quest);
    }
    public void AbandonQuest(Quest quest)
    {
        activeQuests.Remove(quest);
    }
    public bool IsQuestActive(Quest quest)
    {
        return activeQuests.Contains(quest);
    }
    public bool IsQuestCompleted(Quest quest)
    {
        return completedQuests.Contains(quest);
    }
    public void UpdateQuestProgress(Quest quest, int progress)
    {
        //? no way to access the quest progress var
    }
    public List<Quest> GetActiveQuests()
    {
        return activeQuests;
    }
    public List<Quest> GetCompletedQuests()
    {
        return completedQuests;
    }
    public List<Quest> GetAvailableQuests()
    {
        return availableQuests;
    }
    public void AcceptQuest(Quest quest)
    {
        //? What is this supposed to do?
        questChannel.AssignQuest(quest);
    }

    public event Action<Quest> QuestCompletedEvent;


}