using System;

using TMPro;

using UnityEngine;

[Serializable]
public class QuestBox : MonoBehaviour, ISaveable {
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questObjectives;
    [SerializeField] private string defaultPrefix;

    public void SetName(string name) {
        questName.SetText(name);
    }

    public void SetObjectives(string text) {
        questObjectives.SetText(text);
    }

    public void AddObjective(string text) {
        questObjectives.SetText(questObjectives.text + "\n" + defaultPrefix + text);
    }

    public void LoadData(GameData data) {
        data.UIData.UIelementActive.TryGetValue(gameObject.name, out bool active);
        gameObject.SetActive(active);

        data.UIData.UIelementText.TryGetValue(gameObject.name + "-quest", out string questname);
        questName.SetText(questname);

        data.UIData.UIelementText.TryGetValue(gameObject.name + "-objectives", out string objectives);
        questObjectives.SetText(objectives);
    }

    public void SaveData(GameData data) {
        data.UIData.UIelementActive.Add(gameObject.name, gameObject.activeSelf);
        data.UIData.UIelementText.Add(gameObject.name + "-quest", questName.text);
        data.UIData.UIelementText.Add(gameObject.name + "-objectives", questObjectives.text);
    }
}
