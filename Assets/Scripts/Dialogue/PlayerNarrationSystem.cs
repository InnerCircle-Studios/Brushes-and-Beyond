using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNarrationSystem : MonoBehaviour, IObserver 
{
    [SerializeField] Subject _playerSubject;

    public void OnNotify(PlayerActions action) {
        Debug.Log("PlayerNarrationSystem");
    }

    private void OnEnable() {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable() {
        _playerSubject.RemoveObserver(this);
    }

}