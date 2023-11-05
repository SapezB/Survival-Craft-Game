using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;

    GameObject currentWeaponInHand;
    void Start()
    {
       
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
    }


    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealerScript>().StartDealDamage();
    }
   
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealerScript>().EndDealDamage();
    }


}
