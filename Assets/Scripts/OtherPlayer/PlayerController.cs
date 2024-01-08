using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Third Person Controller References
    [SerializeField]
    private Animator playerAnim;

    // Equip-Unequip parameters
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject Potion;
    [SerializeField]
    private GameObject swordOnShoulder;
    [SerializeField]
    private GameObject leftHandHolder;
    [SerializeField]
    private EquipmentHolder equipmentSystem;
 


    private int counter = 0;

    public bool isEquipping;
    public bool isEquipped;
    public bool isPotioning;
    public bool isPotioned;

    // Blocking Parameters
    public bool isBlocking;

    // Kick Parameters
    public bool isKicking;

    // Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;

    private void Start()
    {
        equipmentSystem = this.GetComponent<EquipmentHolder>();
    
    }
    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        Attack();
        Equip();
        EquipPotion();
        Block();
        Kick();
        if (equipmentSystem.SecondaryInvetroySystem.slots.Count != 0)
        {
            if (equipmentSystem.SecondaryInvetroySystem.slots[0].ItemData != null)
            {
                Consume();

            }
        }
       


    
    }


    private void EquipAll(GameObject position)
    {
        equipmentSystem.EnableEquipment(position);

    }
    
    private void Consume()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            equipmentSystem.SecondaryInvetroySystem.slots[0].ItemData.UseItem();

            equipmentSystem.SecondaryInvetroySystem.slots[0].ClearSlot();
            equipmentSystem.SecondaryInvetroySystem.onInvetorySlotChanged?.Invoke(equipmentSystem.SecondaryInvetroySystem.slots[0]);

            //this.GetComponent<HealthSystem>().AddHealth();

        }
        
    }

    private void Equip()

    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            isEquipped = !isEquipped;
            playerAnim.SetBool("isCombat", true);
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
        }
        else
        {
            isEquipped = !isEquipped;
            playerAnim.SetBool("isCombat", false);
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    private void EquipPotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPotioning = !isPotioning; // Toggle potioning state
            ActivePotion(); // Call ActivePotion method to handle the potion's active state
        }
    }

    public void ActivePotion()
    {
        Potion.SetActive(isPotioning); // Set potion active state based on isPotioning
    }

    public void Potioned()
    {
        isPotioning = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void Kick()
    {
        if (Input.GetKey(KeyCode.LeftControl) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }
        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
        {
            if (!isEquipped)
                return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            // Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            // Call Attack Triggers
            playerAnim.SetTrigger("Attack" + currentAttack);

            // Reset Timer
            timeSinceAttack = 0;
        }
    }

    // This will be used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    }
}
