using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class QuestManager : MonoBehaviour {
    [Header("Config")]
    [SerializeField] private bool togglePresistance = true;
    private Dictionary<string, Quest> questMap = new();
    [SerializeField] private Player player;

    private void OnEnable() {
        QuestEvents.OnStartQuest += StartQuest;
        QuestEvents.OnAdvanceQuest += AdvanceQuest;
        QuestEvents.OnFinishQuest += FinishQuest;

        QuestEvents.OnQuestStageStateChanged += QuestStageStateChanged;
    }

    private void OnDisable() {
        QuestEvents.OnStartQuest -= StartQuest;
        QuestEvents.OnAdvanceQuest -= AdvanceQuest;
        QuestEvents.OnFinishQuest -= FinishQuest;

        QuestEvents.OnQuestStageStateChanged -= QuestStageStateChanged;

    }


    private void Awake() {
        questMap = CreateQuestMap();
        player = FindAnyObjectByType<GameManager>().GetPlayer();
    }

    private void Start() {
        questMap.Values.ToList().ForEach(q => {
            if (q.State == QuestState.ACTIVE) {
                q.InitCurrentQuestStep(transform);
            }
            QuestEvents.ChangeQuestState(q);
        });
    }

    private void Update() {
        questMap.Values.ToList().ForEach(q => {
            if (q.State == QuestState.UNAVAILABLE && CheckRequirementsMet(q)) {
                ChangeQuestState(q.Info.Id, QuestState.AVAILVABLE);
            }
        });
    }

    private bool CheckRequirementsMet(Quest quest) {
        bool requirementsMet = true;
        if (player.GetAttrubuteManager().GetAttributes().Level < quest.Info.PlayerStoryLevel) {
            requirementsMet = false;
        }

        quest.Info.RequiredQuests.ToList().ForEach(r => {
            if (GetQuestByID(r.Id).State != QuestState.FINISHED) {
                requirementsMet = false;
            }
        });

        return requirementsMet;
    }


    private void ChangeQuestState(string id, QuestState state) {
        Logger.Log("ChangeQuestState", id + " " + state);
        Quest quest = GetQuestByID(id);
        quest.State = state;
        QuestEvents.ChangeQuestState(quest);
    }

    private void StartQuest(string id) {
        Quest quest = GetQuestByID(id);
        quest.InitCurrentQuestStep(transform);
        ChangeQuestState(id, QuestState.ACTIVE);
    }

    private void AdvanceQuest(string id) {
        Quest quest = GetQuestByID(id);
        quest.MoveToNextStage();
        if (quest.CurrentStageExists()) {
            quest.InitCurrentQuestStep(transform);
        }
        else {
            ChangeQuestState(id, QuestState.FINISHED); // Temporary finished, change to completed later
        }
    }

    private void FinishQuest(string id) {
        Quest quest = GetQuestByID(id);
        ChangeQuestState(id, QuestState.FINISHED);
    }

    private void QuestStageStateChanged(string id, int index, QuestStageState state) {
        // Debug.Log($"[  QuestStageStateChanged  ]: {id} {index} {state.State}");
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStageState(state, index);
        ChangeQuestState(id, quest.State);
    }




    private Dictionary<string, Quest> CreateQuestMap() {
        QuestInfo[] quests = Resources.LoadAll<QuestInfo>("Quests");
        Dictionary<string, Quest> map = new();
        quests.ToList().ForEach(q => {
            if (map.ContainsKey(q.Id)) {
                Logger.LogError("CreateQuestMap",$"Duplicate quest id: {q.Id}");
            }
            map.Add(q.Id, LoadQuest(q));
        });
        return map;
    }

    private Quest GetQuestByID(string id) {
        Quest quest = questMap[id];
        if (quest == null) {
            Logger.LogError("GetQuestByID", $"Quest with id {id} not found");
        }
        return quest;
    }

    private void OnApplicationQuit() {
        questMap.Values.ToList().ForEach(q => {
            SaveQuest(q);
        });
    }

    private void SaveQuest(Quest quest) {
        try {
            QuestData questData = quest.GetQuestData();
            string jsonData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.Info.Id, jsonData);
        }
        catch (Exception e) {
            Logger.LogError("SaveQuest", $" Failed to save quest: {quest.Info.Id} with error: {e}");
        }
    }

    private Quest LoadQuest(QuestInfo questInfo) {
        Quest quest = null;
        try {
            if (PlayerPrefs.HasKey(questInfo.Id) && togglePresistance) {
                string jsonData = PlayerPrefs.GetString(questInfo.Id);
                QuestData questData = JsonUtility.FromJson<QuestData>(jsonData);
                quest = new Quest(questInfo, questData.State, questData.QuestStageIndex, questData.StageStates);
            }
            else {
                quest = new Quest(questInfo);
            }
        }
        catch (Exception e) {
            Logger.LogError("LoadQuest", $" Failed to load quest: {questInfo.Id} with error: {e}");
        }
        return quest;
    }
}