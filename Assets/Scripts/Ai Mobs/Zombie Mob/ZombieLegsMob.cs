using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float health = 100f;
    public float attackRange = 2f;
    public float walkRadius = 10f;
    public float attackDamage = 10f;

    private NavMeshAgent agent;
    private float distanceToPlayer;
    private bool isDead = false;
    private float timeSinceLastAttack = 0f;
    private float attackCooldown = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Wander", 0f, 5f); // Call Wander every 5 seconds
    }

    void Update()
    {
        if (isDead) return;

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer < 20f) // Chase player range
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Wander()
    {
        if (isDead || distanceToPlayer < 20f) return;

        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
        animator.Play("Zombie@Walk");
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            animator.Play("Zombie@Idle");
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.Play("Zombie@Run");
    }

    void AttackPlayer()
    {
        if (timeSinceLastAttack > attackCooldown)
        {
            animator.Play("Zombie@Attack1");
            // Implement the logic to reduce player health here
            timeSinceLastAttack = 0f;
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            animator.Play("Zombie@Hit1"); // Play hit animation
        }
    }

    void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.Play("Zombie@Death");
        // Optionally disable the zombie after a delay
        // Destroy(gameObject, 2f);
    }
}

