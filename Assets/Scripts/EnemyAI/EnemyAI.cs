using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{

    //System Variables
    [SerializeField] private float roamDistance = 5.0f;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float detectionRadius = 3.0f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.0f; // Time between attacks
    [SerializeField] private bool isMelee = false;
    //Movement and detectection
    private Rigidbody2D rb;
    public Shootah Enemy;
    private Vector2 startingPosition;
    private Vector2 roamPosition;
    private Transform player;
    //Attack Variables
    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Chasing,
        Attacking,
        Returning,
        Dead
    }

    private State currentState;

    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewRoamingDestination();
        currentState = State.Roaming;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (GetComponent<AttributeManager>().CurrentHealth <= 0)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.Roaming:
                Roam();
                if (distanceToPlayer < detectionRadius)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                ChasePlayer();
                if (distanceToPlayer < attackRange && canAttack)
                {
                    currentState = State.Attacking;
                }
                else if (distanceToPlayer > detectionRadius * 2)
                {
                    currentState = State.Returning;
                }
                break;

            case State.Attacking:
                if (isMelee)
                {
                    // MeleeAttack();
                }
                else
                {
                    RangedAttack();
                }
                break;

            case State.Returning:
                ReturnToStart();
                if (Vector2.Distance(transform.position, startingPosition) < 0.1f)
                {
                    currentState = State.Roaming;
                }
                break;

            case State.Dead:
                Die();
                break;
        }
    }

    void Roam()
    {
        Vector2 direction = (roamPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, roamPosition) < 0.1f)
        {
            PickNewRoamingDestination();
            rb.velocity = Vector2.zero; // Stop moving once the destination is reached
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void ReturnToStart()
    {
        Vector2 direction = (startingPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, startingPosition) < 0.1f)
        {
            rb.velocity = Vector2.zero; // Stop moving once back at the starting position
        }
    }


    void PickNewRoamingDestination()
    {
        float roamX = startingPosition.x + Random.Range(-roamDistance, roamDistance);
        float roamY = startingPosition.y + Random.Range(-roamDistance, roamDistance);
        roamPosition = new Vector2(roamX, roamY);
    }



    void RangedAttack()
    {
        if (canAttack)
        {
            Enemy.Shoot();
            Debug.Log("Enemy Shoot");
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        currentState = State.Chasing; // After the attack, go back to chasing
    }




    private void Die()
    {
        Destroy(gameObject, 1.0f);
    }
}
