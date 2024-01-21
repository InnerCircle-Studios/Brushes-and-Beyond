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
        _HostileStateMachine.CheckPlayerInAttackRange();
        CheckSwitchStates();
        if(!_canAttack && _attackAnimationDone) {
            GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Idle");
        }
        if (_canAttack)
        {
            
            Attack();
            GetStateMachine().GetActor().StartCoroutine(WaitforAttackAnimation());
            GetStateMachine().GetActor().StartCoroutine(WaitForAttackcooldown());
        }
        
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
        _canAttack = false;

        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }

    private IEnumerator WaitforAttackAnimation() 
    {
        _attackAnimationDone = false;
        yield return new WaitForSeconds(0.85f);
        _attackAnimationDone = true;
    }

    private void Attack()
    {  

        switch(_HostileStateMachine._Colour)
        {
            case "Red":
                GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Shoot");
                _HostileStateMachine._Hostile.HandleMeleeAttack();
                break;

            case "Yellow":
                GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Shoot");
                _HostileStateMachine._Hostile.HandleRangedAttack();
                break;

            case "Blue":
                GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Shoot");
                _HostileStateMachine._Hostile.HandleMeleeAttack();
                _HostileStateMachine._isDead.Value = true;
                break;
        }
    }

    private bool _attackAnimationDone = false;
    private float _attackCooldown = 3f;
    private bool _canAttack = true;
    private HostileStateMachine _HostileStateMachine;
}
