using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IObserver
{
    public DialogueTrigger trigger;
    [SerializeField] Subject _playerSubject;
    [SerializeField] PlayerStateMachine _playerStateMachine;

    public void OnNotify(PlayerActions action)
    {
        if (action == PlayerActions.Dialogue)
        {
            Debug.Log("Player has notified NPC");
            trigger.StartDialogue();
        }
    }
    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
