using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] private PlayerHealthBar _healthBar;
    [SerializeField] GameObject hitVFX;
    private bool hasDied = false;
    private float health;
    public GameObject deathScreen;
    //[SerializeField] GameObject ragdoll;

    Animator animator;
    void Start()
    {
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (hasDied) return;
        health -= damageAmount;
        _healthBar.UpdateHealthBar(maxHealth, health);
        animator.SetTrigger("damage");


        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (hasDied) return;
        //Instantiate(ragdoll, transform.position, transform.rotation);
        animator.SetTrigger("dead");
        StartCoroutine(DestroyAfterDelay(4.1f));
        hasDied = true;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        deathScreen.SetActive(true);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        if (hasDied) return;    
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);

    }

    public void AddHealth()
    {
        
       health += 10;
        
    }
}