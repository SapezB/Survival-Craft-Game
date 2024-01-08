using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] private PlayerHealthBar _healthBar;
    [SerializeField] float maxHunger = 100;
    [SerializeField] private PlayerHungerBar _hungerBar;
    [SerializeField] float hunger;
    [SerializeField] float hungerDepletionRate = 0; // Hunger depletion per second
    [SerializeField] float healthDepletionRate = 0; // Health depletion per second when hungry
    [SerializeField] float healingRate = 1; // Health gain per second when well-fed
    [SerializeField] GameObject hitVFX;
    private bool hasDied = false;
    public float health;
    Animator animator;

    // XP System
    public int xp = 0; // Player's current XP
    public int xpForNextLevel = 1; // XP needed for next level
    public DamageDealerScript damageDealer;

    void Start()
    {
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        health = maxHunger;
        _hungerBar.UpdateHungerBar(maxHunger, hunger);
        animator = GetComponent<Animator>();
        hunger = maxHunger; // Initialize hunger to full
       
        damageDealer = GetComponentInChildren<DamageDealerScript>();
    }

    public void GainXP(int amount)
    {
        xp += amount;
        if (xp >= xpForNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        xp -= xpForNextLevel;
        xpForNextLevel++; // Increase the XP required for next level

        maxHealth += 10; // Increase max health
        damageDealer.weaponDamage += 1; // Increase weapon damage
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
        animator.SetTrigger("dead");
        StartCoroutine(DestroyAfterDelay(4.1f));
        hasDied = true;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        if (hasDied) return;
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    // Add methods to increase hunger (e.g., when eating food)
    
}