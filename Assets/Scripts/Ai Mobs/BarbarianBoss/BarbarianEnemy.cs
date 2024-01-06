using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianEnemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] GameObject hitVFX;
    

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    [SerializeField] private float rotationSpeed = 5f;


    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    private bool hasDied = false;
    private int axeAttackCount = 0;
    private int hitCounter = 0; // To count the hits from the player
    private float health;
    private bool buffApplied = false;
    private bool stunApplied = false;

    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (player == null || hasDied)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Smoothly rotate towards the player when in attack range
            LookAtPlayer();

            if (timePassed >= attackCD)
            {
                if (axeAttackCount >= 2)
                {
                    transform.LookAt(transform.position);
                    animator.SetTrigger("shieldattack"); // Trigger shield attack animation
                    axeAttackCount = 0; // Reset the axe attack counter
                }
                else
                {
                    animator.SetTrigger("attack"); // Regular axe attack
                    axeAttackCount++; // Increment axe attack counter
                }
                timePassed = 0;
            }
        }
        else if (distanceToPlayer <= aggroRange)
        {
            // Optionally, handle behavior when player is within aggro range but outside attack range
            // For example, moving towards the player or some other action
        }

        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && distanceToPlayer <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Smooth rotation
    }

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
        hitCounter++;

        // Hit animation every third hit
        if (hitCounter % 3 == 0)
        {
            animator.SetTrigger("hit");
        }

        // Stunned animation at half health
        if (health <= maxHealth / 2 && !stunApplied)
        {
            animator.SetTrigger("stuned");
            stunApplied = true;
        }

        // Buff animation and heal at 20% health
        if (health <= maxHealth * 0.2 && !buffApplied)
        {
            animator.SetTrigger("buff");
            health += maxHealth * 0.2f; // Heal for 20% of max health
            attackCD *= 0.8f; // Decrease attack cooldown by 20%
            buffApplied = true; // Ensure buff is only applied once
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
        GetComponentInChildren<ShieldDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
        GetComponentInChildren<ShieldDamageDealer>().EndDealDamage();
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