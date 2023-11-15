using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class Combat : ICombat
{
    public List<Actor> MeleeAttack(Vector2 position, float attackRange, string layer)
    {
        List<Actor> hitActors = new List<Actor>();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, attackRange, new UnityEngine.Vector3(1, 0, 0), 0f, LayerMask.GetMask(layer)).Distinct().ToArray();

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.transform.CompareTag(layer))
            {
                hitActors.Add(hit.collider.gameObject.GetComponent<Actor>());
            }
        }

        return hitActors;
    }

    public Actor RangedAttack(Vector2 position)
    {
        return null;
    }
}