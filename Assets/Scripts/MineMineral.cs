using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMineral : MonoBehaviour
{
    public float health = 3;

    private void Die()
    {
       Destroy(this.gameObject);
        
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
