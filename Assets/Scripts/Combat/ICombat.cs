using System.Collections.Generic;

using UnityEngine;

public interface ICombat {
    public List<Actor> MeleeAttack(Vector2 position, float attackRange, string layer);

    public Actor RangedAttack(Vector2 position);
}