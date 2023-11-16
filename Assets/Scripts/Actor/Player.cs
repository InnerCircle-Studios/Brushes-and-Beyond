using UnityEngine;

public class Player : Actor
{
    public override void Start()
    {
        _PlayerStateMachine = new PlayerStateMachine(this);
    }

    public override void Update()
    {
        _PlayerStateMachine.GetCurrentState().UpdateState();
    }

    public override void HandleMeleeAttack()
    {
        foreach (Actor hits in GetCombat().MeleeAttack(new Vector2(0,0), 1.5f, "Player"))
        {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
        }
    }

    public override void HandleRangedAttack() 
    {
        
    }

    private PlayerStateMachine _PlayerStateMachine;
}