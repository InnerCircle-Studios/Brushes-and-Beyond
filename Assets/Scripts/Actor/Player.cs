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
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position , 1.5f, "Enemy"))
        {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
        }
    }

    public override void HandleRangedAttack() 
    {
        
    }

    public void OnDeath()
    {
        GetEventManager().OnDeath(true);
    }

    private PlayerStateMachine _PlayerStateMachine;
}