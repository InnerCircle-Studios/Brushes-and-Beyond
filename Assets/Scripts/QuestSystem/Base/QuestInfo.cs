using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Brushes/Quests/QuestInfo", order = 0)]
public class QuestInfo : ScriptableObject {
    [field: SerializeField] public string Id { get; private set; }

    [Header("General")]
    public string DisplayName;

    [Header("Requirements")]
    public int PlayerStoryLevel;
    public QuestInfo[] RequiredQuests;

    [Header("Stages")]
    public GameObject[] Stages;

    [Header("Rewards")]
    public GameObject RewardItem;


    private void OnValidate() {
        #if UNITY_EDITOR
        Id = name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}