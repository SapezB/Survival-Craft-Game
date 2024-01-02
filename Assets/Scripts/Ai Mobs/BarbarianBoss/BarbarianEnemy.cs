using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianEnemy : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    private bool hasDied = false;
    private int axeAttackCount = 0;

    void Start()
    {
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

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                if (axeAttackCount >= 2)
                {
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
        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }


    private void Die()
    {
        if (hasDied) return;
        animator.SetTrigger("death"); // Trigger death animation using a trigger
        agent.isStopped = true; // Stop the NavMeshAgent
        hasDied = true;

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
        animator.SetTrigger("damage");
       

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