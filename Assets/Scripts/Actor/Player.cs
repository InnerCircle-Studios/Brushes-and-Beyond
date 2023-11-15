using UnityEngine;

public class Player : Actor
{
    public override void Start()
    {

    }

    public override void Update()
    {

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
}