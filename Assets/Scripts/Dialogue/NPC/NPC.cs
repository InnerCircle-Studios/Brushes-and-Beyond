using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IObserver
{
    public DialogueTrigger trigger;
    [SerializeField] Subject _playerSubject;
    private bool Collided = false;

    public void OnNotify(PlayerActions action)
    {
        if (action == PlayerActions.Dialogue && Collided)
        {
            Debug.Log("Player has notified NPC");
            trigger.StartDialogue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collided = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collided = false;
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
