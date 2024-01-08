using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{
    [SerializeField] private static float maxHealth = 3;
    public GameObject drop;
    public GameObject appleDrop;
    private float health = maxHealth;
    

  
    private void Die()
    {
       Destroy(this.gameObject);
       for (int i = 0; i < maxHealth; i++)
       {
        Instantiate(drop, transform.position, Quaternion.identity);
       }
       if(Random.Range(0,2) == 1){
        Instantiate(appleDrop, transform.position, Quaternion.identity);
       }
        
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }
}
