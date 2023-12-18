using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class Combat : ICombat {
    public List<Actor> MeleeAttack(Vector2 position, float attackRange, string layer) {
        List<Actor> hitActors = new();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, attackRange, new Vector3(1, 0, 0), 0f, LayerMask.GetMask(layer)).Distinct().ToArray();

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.CompareTag(layer)) {
                Debug.Log(hit.collider.gameObject.name);
                hitActors.Add(hit.collider.gameObject.GetComponent<Actor>());
            }
        }

        return hitActors;
    }

    public Actor RangedAttack(Vector2 position) {
        return null;
    }
}