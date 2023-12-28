using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearEnemy : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject ragdoll;

    [Header("Combat")]
    [SerializeField] private float attackCD = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float aggroRange = 4f;
    [SerializeField] private float attackDamage = 10f; // Add attack damage

    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private float timePassed;
    private bool isStunned = false;
    private bool isDead = false;
    private int attackCounter = 0;
    private EnemyDamageDealer damageDealer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        damageDealer = GetComponentInChildren<EnemyDamageDealer>(); // Assumes EnemyDamageDealer is a child

        // Start with the bear sleeping
        animator.Play("Bear_Sleep");
    }

    void Update()
    {
        if (isDead) return;

        if (health <= 0)
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!isStunned)
        {
            HandleMovementAndAttacks(distanceToPlayer);
        }
        else
        {
            HandleStun();
        }

        timePassed += Time.deltaTime;
    }

    private void HandleMovementAndAttacks(float distanceToPlayer)
    {
        if (distanceToPlayer <= aggroRange && animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Sleep"))
        {
            animator.Play("Bear_Buff");
            StartCoroutine(WaitAndRun(2f)); // Wait for 2 seconds before running
        }
        else if (distanceToPlayer <= attackRange && timePassed >= attackCD)
        {
            AttackPlayer();
            timePassed = 0;
        }
        else if (distanceToPlayer <= aggroRange)
        {
            agent.SetDestination(player.transform.position);
            animator.Play("Bear_RunForward");
        }
    }

    private IEnumerator WaitAndRun(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.Play("Bear_RunForward");
    }

    private void HandleStun()
    {
        if (timePassed >= 5f) // Stunned for 5 seconds
        {
            isStunned = false;
            animator.Play("Bear_Buff");
            attackCD -= 0.5f; // Increase attack speed
            timePassed = 0;
        }
        else
        {
            timePassed += Time.deltaTime;
        }
    }

    private void AttackPlayer()
    {
        if (attackCounter < 2)
        {
            animator.Play("Bear_Attack1");
            damageDealer.StartDealDamage(); // Start dealing damage
            attackCounter++;
        }
        else
        {
            animator.Play("Bear_Attack5");
            damageDealer.StartDealDamage(); // Start dealing damage
            attackCounter = 0;
        }
    }

    public void EndAttack()
    {
        damageDealer.EndDealDamage(); // End dealing damage
    }


    private void Die()
    {
        animator.Play("Bear_Death");
        isDead = true;
        Destroy(gameObject, 3f); // Adjust delay as needed
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;
        animator.Play("Bear_GetHitFromFront");

        if (health <= 50 && !isStunned)
        {
            isStunned = true;
            animator.Play("Bear_StunnedLoop");
            timePassed = 0;
        }
    }


    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    //public void HitVFX(Vector3 hitPosition)
    //{
    //GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
    //Destroy(hit, 3f);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}

    // Existing functions StartDealDamage, EndDealDamage, HitVFX, OnDrawGizmos can remain as they are


