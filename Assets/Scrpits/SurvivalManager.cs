using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDepletionRate = 1f;
    private float currentHunger;
    public float HungerPercent => currentHunger/maxHunger;

    public static UnityAction OnPlayerDeath;

    [Header("Health")]
    [SerializeField] private float maxHealth =100f;
    [SerializeField] private float healthDepletionRate = 1f;
    private float currentHealth;
    public float HealthPercent => currentHealth / maxHealth;


    private void Start()
    {
        currentHunger = maxHunger;
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
}
