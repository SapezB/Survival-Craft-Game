using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger =100f;
    [SerializeField] private float hungerDelplationRate =1f;
    private float currentHunger;
    public float HungerPercent => currentHunger / maxHunger;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthDelplationRate = 1f;
    private float currentHealth;
    public float HealthPercent => currentHealth/ maxHealth;

    public static UnityAction OnPlayerDeath;


    private void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
    }

    private void Update()
    {
        currentHunger -= hungerDelplationRate * Time.deltaTime;

        if (currentHealth <= 0 || currentHunger <=0)
        {
            OnPlayerDeath?.Invoke();
            currentHunger = 0;
            currentHealth = 0;
        }


    }

    public void ReplenishHunger(float hungerValue)
    {
        currentHealth += hungerValue;

        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
    }
}
