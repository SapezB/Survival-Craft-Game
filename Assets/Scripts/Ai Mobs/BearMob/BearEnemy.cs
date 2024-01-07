using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] LootBag lootBag;

    [Header("Combat")]
    [SerializeField] private float attackCD = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float aggroRange = 4f;

    private float health;
    private int hitCounter = 0;
    private bool hasDied = false;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private float timePassed;
    private bool isSleeping = true;
    private bool canMove = true; // Added to control movement after buff

    void Start()
    {
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("isSleeping", true); // Start with the bear sleeping
    }

    

    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (player == null || hasDied)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            if (isSleeping)
            {
                isSleeping = false;
                
                animator.SetBool("isSleeping", false);
                LookAtPlayer(); // First, turn towards the player
                StartCoroutine(PerformBuff()); // Then perform buff
            }

            if (canMove)
            {
                LookAtPlayer();
                agent.SetDestination(player.transform.position); // Move towards player
                AttemptAttack(distanceToPlayer);
            }
        }
        else if (!isSleeping)
        {
            // Reset to sleeping state if player leaves aggro range
            
            animator.SetBool("isSleeping", true);
            isSleeping = true;
            canMove = true;
            agent.ResetPath();
        }
    }

    private IEnumerator PerformBuff()
    {
        animator.SetTrigger("buff");
        canMove = false;
        yield return new WaitForSeconds(2f); // Duration of the buff animation
        canMove = true;
        // Transition to combat idle here if needed
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
    }

    private void AttemptAttack(float distanceToPlayer)
    {
        if (timePassed >= attackCD && distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("attack");
            timePassed = 0;
        }
        timePassed += Time.deltaTime;
    }

    // Call this method using an Animation Event at the end of the buff animation



    private void Die()
    {
       
        if (hasDied) return;
        animator.SetTrigger("death"); // Trigger death animation using a trigger
        agent.isStopped = true; // Stop the NavMeshAgent
        hasDied = true;

        GetComponent<LootBag>().InstantitateLoot(transform.position);
        StartCoroutine(DestroyAfterDelay(3.5f)); // Adjust the time to match the length of the death animation
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    public void TakeDamage(float damageAmount)
    {
        if (hasDied) return;

        health -= damageAmount;
        _healthBar.UpdateHealthBar(maxHealth, health);

        hitCounter++; // Increment hit counter

        if (hitCounter >= 3)
        {
            animator.SetTrigger("damage"); // Perform damage animation every third hit
            hitCounter = 0; // Reset hit counter
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<BearDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<BearDamageDealer>().EndDealDamage();
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}