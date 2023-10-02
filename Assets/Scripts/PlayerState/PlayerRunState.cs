using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory){}
    
    public override void EnterState(){
        Debug.Log("PlayerRunState");
    }
    public override void UpdateState(){
        HandleRun();
        CheckSwitchStates();
    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.IsMovementPressed == false)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsRunningPressed == false)
        {
            SwitchState(Factory.Walk());
        }
        else if(Ctx.IsAttackPressed){
            SwitchState(Factory.Jump());
        }
    }
    public override void InitializeSubStates(){}

    void HandleRun()
    {
        Ctx.AppliedMovement = new Vector3(Ctx.CurrentMovementInput.x, 0, Ctx.CurrentMovementInput.y).normalized;
        Ctx.transform.Translate(Ctx.AppliedMovement * Ctx.SprintSpeed * Time.deltaTime);
    }

}
