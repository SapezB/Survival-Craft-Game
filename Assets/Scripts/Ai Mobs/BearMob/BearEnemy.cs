using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearEnemy : MonoBehaviour
{
    [SerializeField] private float health = 3;
    [SerializeField] private GameObject hitVFX;

    [Header("Combat")]
    [SerializeField] private float attackCD = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float aggroRange = 4f;

    private bool hasDied = false;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private float timePassed;
    private bool isSleeping = true;

    void Start()
    {
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
                animator.SetTrigger("buff"); // Trigger buff animation
                animator.SetBool("isSleeping", false);
                isSleeping = false;
            }

            // Bear looks at and moves towards the player
            LookAtPlayer();
            agent.SetDestination(player.transform.position);

            AttemptAttack(distanceToPlayer);
        }
        else if (!isSleeping)
        {
            animator.SetBool("isSleeping", true); // Go back to sleeping state
            isSleeping = true;
            agent.ResetPath(); // Stop the bear from moving
        }
    }

    private void LookAtPlayer()
    {
        // Optionally, you can adjust this to only rotate on the Y axis
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
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