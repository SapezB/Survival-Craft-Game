using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerScript : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] private float weaponDamage;


    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    public void IncreaseWeaponDamage(float amount)
    {
        weaponDamage += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            int layerMask = 1 << 7; // Make sure your passive mobs are on this layer
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    if (hit.transform.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(weaponDamage);
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                    else if (hit.transform.TryGetComponent(out PassiveEnemy passiveEnemy))
                    {
                        passiveEnemy.TakeDamage(weaponDamage);
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                    else if (hit.transform.TryGetComponent(out BearEnemy bearEnemy))
                    {
                        bearEnemy.TakeDamage(weaponDamage);
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                    else if (hit.transform.TryGetComponent(out BarbarianEnemy barbarianEnemy))
                    {
                        barbarianEnemy.TakeDamage(weaponDamage);
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                    else if (hit.transform.TryGetComponent(out ChopTree tree))
                    {
                        tree.TakeDamage(weaponDamage);
                    }
                    else if (hit.transform.TryGetComponent(out MineMineral mineral))
                    {
                        mineral.TakeDamage(weaponDamage);
                    }
                }
            }
        }
    }


    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}