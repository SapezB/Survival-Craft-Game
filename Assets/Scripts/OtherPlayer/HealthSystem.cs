using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] private PlayerHealthBar _healthBar;
    [SerializeField] float maxHunger = 100;
    [SerializeField] private PlayerHungerBar _hungerBar;
    [SerializeField] float hunger;
    [SerializeField] float hungerDepletionRate = 1; // Hunger depletion per second
    [SerializeField] float healthDepletionRate = 5; // Health depletion per second when hungry
    [SerializeField] float healingRate = 1; // Health gain per second when well-fed
    [SerializeField] GameObject hitVFX;
    private bool hasDied = false;
    public float health;
    Animator animator;

    // XP System
    public int xp = 0; // Player's current XP
    public int xpForNextLevel = 1; // XP needed for next level
    public DamageDealerScript damageDealer;
    public GameObject loseScreen;
    void Start()
    {
        health = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, health);
        health = maxHunger;
        _hungerBar.UpdateHungerBar(maxHunger, hunger);
        animator = GetComponent<Animator>();
        hunger = maxHunger; // Initialize hunger to full
        StartCoroutine(HungerRoutine());
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
        damageDealer.IncreaseWeaponDamage(1); // Increase weapon damage
    }

    IEnumerator HungerRoutine()
    {
        while (!hasDied)
        {
            UpdateHunger();
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateHunger()
    {
        if (hunger > 0)
        {
            hunger -= hungerDepletionRate;
            _hungerBar.UpdateHungerBar(maxHunger, hunger);
        }
        else if (health > 0)
        {
            // Start losing health when hunger is at 0
            health -= healthDepletionRate;
            if (health <= 0)
            {
                Die();
            }
        }

        // Heal if hunger is above 80%
        if (hunger > maxHunger * 0.8 && health < 100)
        {
            health += healingRate;
            health = Mathf.Min(health, 100); // Ensure health does not exceed 100
        }
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
        loseScreen.SetActive(true);
        Destroy(this.gameObject);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        if (hasDied) return;
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    // Add methods to increase hunger (e.g., when eating food)
    public void EatFood(float foodValue)
    {
        hunger += foodValue;
        hunger = Mathf.Min(hunger, maxHunger); // Ensure hunger does not exceed max
    }
}
