using UnityEngine;
using UnityEngine.Events;

public class InkyDialogue : MonoBehaviour {
    public DialogueTrigger FirstDialogue;
    public GameObject objectPrefab;
    public GameObject panel1;
    public GameObject panel2;
    [SerializeField] private UnityEvent giveMaxPaints;

    public void DialogueInky() {
        FirstDialogue.StartDialogue();
    }

    public void DestroyInky() {
        Destroy(gameObject);
    }

    public void GiveMaxPaints() {
        for(int i = 0; i < 3; i++) {
            giveMaxPaints.Invoke();
        }
    }


    // Function to show the panels
    public void ShowPanelOne() {
        if (panel1 != null) panel1.SetActive(true);
    }
    public void ShowPanelTwo() {
        if (panel2 != null) panel2.SetActive(true);
    }

    // Function to hide the panels
    public void HidePanels() {
        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);
    }



}