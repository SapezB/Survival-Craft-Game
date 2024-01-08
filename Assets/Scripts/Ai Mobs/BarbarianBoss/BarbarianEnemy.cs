using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianEnemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] private HealthBar _healthBar;
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
    private bool isStunnedOrBuffed = false; // New variable to check if stunned or buffed
    [SerializeField] private float resumeDelay = 2f; // Delay after which boss resumes behavior
    public GameObject winScreen;

    void Start()
    {
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (player == null || hasDied || isStunnedOrBuffed)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            LookAtPlayer();

            if (timePassed >= attackCD)
            {
                AttackPlayer();
            }
        }
        else if (distanceToPlayer <= aggroRange)
        {
            agent.SetDestination(player.transform.position);
        }

        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && distanceToPlayer <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
    }

    private void AttackPlayer()
    {
        if (axeAttackCount >= 2)
        {
            animator.SetTrigger("shieldattack");
            axeAttackCount = 0;
        }
        else
        {
            animator.SetTrigger("attack");
            axeAttackCount++;
        }
        timePassed = 0;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Die()
    {
        if (hasDied) return;
        animator.SetTrigger("death");
        agent.isStopped = true;
        hasDied = true;
        GetComponent<LootBag>().InstantitateLoot(transform.position);
        StartCoroutine(DestroyAfterDelay(3.5f));
        winScreen.SetActive(true);

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
        hitCounter++;

        if (hitCounter % 3 == 0)
        {
            animator.SetTrigger("damage");
        }

        if (health <= maxHealth / 2 && !stunApplied)
        {
            isStunnedOrBuffed = true;
            animator.SetTrigger("stuned");
            StartCoroutine(ResumeAfterDelay(resumeDelay));
            stunApplied = true;
        }

        if (health <= maxHealth * 0.2 && !buffApplied)
        {
            isStunnedOrBuffed = true;
            animator.SetTrigger("buff");
            health += maxHealth * 0.2f;
            attackCD *= 0.8f;
            buffApplied = true;
            StartCoroutine(ResumeAfterDelay(resumeDelay));
            _healthBar.UpdateHealthBar(maxHealth, health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStunnedOrBuffed = false;
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
