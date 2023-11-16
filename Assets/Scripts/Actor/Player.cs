using UnityEngine;

public class Player : Actor
{
    public override void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();

        _PlayerStateMachine = new PlayerStateMachine();
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

    private Rigidbody2D _RigidBody;

    private PlayerStateMachine _PlayerStateMachine;
}