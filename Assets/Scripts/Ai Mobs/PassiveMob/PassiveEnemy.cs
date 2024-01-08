using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class PassiveEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private float roamingRadius = 10f;
    [SerializeField] private float roamingInterval = 5f;

    private bool hasDied = false;
    private NavMeshAgent agent;
    private Animator animator;
    private float timeToNextRoaming = 0f;
    private float health;

    void Start()
    {
        StartCoroutine(waitfewseconds());
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timeToNextRoaming = roamingInterval;
    }

    IEnumerator waitfewseconds(){
        yield return new WaitForSeconds(3);
    }

    void Update()
    {
        // Update speed parameter in the animator
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        // Roaming logic
        timeToNextRoaming -= Time.deltaTime;
        if (timeToNextRoaming <= 0)
        {
            Roam();
            timeToNextRoaming = roamingInterval;
        }
    }

    private void Roam()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamingRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamingRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
    }

    public void TakeDamage(float damageAmount)
    {
        if (hasDied) return; // No further action if already dead

        health -= damageAmount;
        _healthBar.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("damage"); // Trigger hit reaction animation
            if (hitVFX != null)
            {
                Instantiate(hitVFX, transform.position, Quaternion.identity); // Play hit VFX
            }
        }
    }

    private void Die()
    {
        if (hasDied) return;
        animator.SetTrigger("death"); // Trigger death animation using a trigger
        agent.isStopped = true; // Stop the NavMeshAgent
        hasDied = true;
        GetComponent<LootBag>().InstantitateLoot(transform.position);
        StartCoroutine(DestroyAfterDelay(2f)); // Adjust the time to match the length of the death animation
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere in the Scene view, representing the roaming radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, roamingRadius);
    }
}
