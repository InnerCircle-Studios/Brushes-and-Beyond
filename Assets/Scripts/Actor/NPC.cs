using System;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class NPC : Actor {

    public override void Start() {
        _NPCStateMachine = new NpcStateMachine(this);
    }

    public override void Update() {
        _NPCStateMachine.GetCurrentState().UpdateState();
    }

    public override void HandleMeleeAttack() { }

    public override void HandleRangedAttack() { }

    private NpcStateMachine _NPCStateMachine;
}