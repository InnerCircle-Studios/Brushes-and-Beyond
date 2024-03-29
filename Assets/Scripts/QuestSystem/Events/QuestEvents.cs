using System;
using System.Collections.Generic;

using UnityEngine;

public static class QuestEvents {
    //Quest events, also known as: I made this static because I 
    //couldn't be bothered to actually fix the GameManager
    public static event Action<string> OnStartQuest;
    public static void StartQuest(string id) => OnStartQuest?.Invoke(id);

    public static event Action<string> OnAdvanceQuest;
    public static void AdvanceQuest(string id) => OnAdvanceQuest?.Invoke(id);

    public static event Action<string> OnFinishQuest;
    public static void FinishQuest(string id) => OnFinishQuest?.Invoke(id);


    public static event Action<Quest> OnQuestStateChanged;
    public static void ChangeQuestState(Quest quest) => OnQuestStateChanged?.Invoke(quest);

    public static event Action<string, int, QuestStageState> OnQuestStageStateChanged;
    public static void ChangeQuestStageState(string id, int index, QuestStageState state) => OnQuestStageStateChanged?.Invoke(id, index, state);


    public static event Action<Dictionary<string, DialogueSet>> OnChangeDialogue;
    public static void ChangeDialogue(Dictionary<string, DialogueSet> data) => OnChangeDialogue?.Invoke(data);

    public static event Action<Dictionary<string, DialogueSet>> OnOverrideBaseDialogue;
    public static void OverrideBaseDialogue(Dictionary<string, DialogueSet> data) => OnOverrideBaseDialogue?.Invoke(data);

    public static event Action<bool> OnSetDialogueAdvanceable;
    public static void SetDialogueAdvanceable(bool value) => OnSetDialogueAdvanceable?.Invoke(value);

}