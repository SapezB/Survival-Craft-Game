using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{
    [SerializeField] public  float health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Die()
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
