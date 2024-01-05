using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDepletionRate = 1f;
    private float currentHunger;
    public float HungerPercent => currentHunger/maxHunger;
    public TextMeshProUGUI healthText;

    public static UnityAction OnPlayerDeath;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthDepletionRate = 1f;
    public int damagePerHit = 10; 
    public float currentHealth;
    public float HealthPercent => currentHealth / maxHealth;


    private void Start()
    {
     
        currentHealth = maxHealth;
    

    }

    private void Update()
    {
        

        currentHunger -= hungerDepletionRate * Time.deltaTime;

        if (currentHealth <= 0 || currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
            currentHealth = 0;
            currentHunger = 0;
        }
    }


    public void ReplenishHunger(float hunger)
    {
        currentHunger += hunger;

        if (currentHunger >= maxHunger) { 
        currentHealth = maxHealth;
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnedObject"))
        {
            TakeDamage(damagePerHit);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health : " + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       SceneManager.LoadSceneAsync(0);
    }
}
