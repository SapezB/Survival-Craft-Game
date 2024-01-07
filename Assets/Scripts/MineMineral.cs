using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMineral : MonoBehaviour
{
    public float health = 3;
    public GameObject drop;

    private void Die()
    {
       Destroy(this.gameObject);
       for (int i = 0; i < Random.Range(1,4); i++)
       {
        Instantiate(drop, transform.position, Quaternion.identity);
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
