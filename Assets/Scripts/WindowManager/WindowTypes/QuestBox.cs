using System;

using TMPro;

using UnityEngine;

[Serializable]
public class QuestBox : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questObjectives;
    [SerializeField] private string defaultPrefix;

    public void SetName(string name){
        questName.SetText(name);
    }

    public void SetObjectives(string text){
        questObjectives.SetText(text);
    }

    public void AddObjective(string text){
        questObjectives.SetText(questObjectives.text+"\n"+defaultPrefix+text);
    }

}
