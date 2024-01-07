using UnityEngine;
using System.Collections;

public class HostileAttackState : State
{
    public HostileAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if (_canAttack)
        {
            Attack();
            _canAttack = false;
        }
        else
        {
            GetStateMachine().GetActor().StartCoroutine(WaitForAttackcooldown());
        }
        _HostileStateMachine.CheckPlayerInAttackRange();
        CheckSwitchStates();
    } 

    public override void ExitState()
    {
        
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isDead, true), _HostileStateMachine.GetState("HostileDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isInAttackRange, false), _HostileStateMachine.GetState("HostileWalkState"));
    }


    private IEnumerator WaitForAttackcooldown() 
    {
        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }

    private void Attack()
    {  

        switch(_HostileStateMachine._Colour)
        {
            case "Red":
                _HostileStateMachine._Hostile.HandleMeleeAttack();
                break;

            case "Yellow":
                _HostileStateMachine._Hostile.HandleMeleeAttack();
                break;

            case "Blue":
                GetStateMachine().GetActor().GetAnimator().Play("Shoot");
                _HostileStateMachine._Hostile.HandleRangedAttack();
                break;
        }
    }


    private float _attackCooldown = 1f;
    private bool _canAttack = true;
    private HostileStateMachine _HostileStateMachine;
}
