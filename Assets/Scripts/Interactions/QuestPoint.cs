using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class QuestPoint : MonoBehaviour {

    [Header("Quest")]
    [SerializeField] private QuestInfo questInfo;

    private string questId;
    private QuestState currentState;

    private void Awake() {
        questId = questInfo.Id;
    }

    private void OnEnable() {
        QuestEvents.OnQuestStateChanged += ChangeQuestState;
    }

    private void OnDisable() {
        QuestEvents.OnQuestStateChanged -= ChangeQuestState;
    }

    public void Activate() {

        if (currentState == QuestState.AVAILVABLE) {
            QuestEvents.StartQuest(questId);
        }
        else if (currentState == QuestState.COMPLETED) {
            QuestEvents.FinishQuest(questId);
        }
    }


    private void ChangeQuestState(Quest quest) {
        if (quest.Info.Id == questId) {
            currentState = quest.State;
            // Debug.Log($"Quest {questId} state changed to {currentState}");
        }
    }
}