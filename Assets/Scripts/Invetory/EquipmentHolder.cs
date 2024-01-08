using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class EquipmentHolder : Holder
{
    [SerializeField] protected int secondaryInventroySize;
    [SerializeField] protected InventorySystem secondaryInvetroySystem;
    [SerializeField] public List<GameObject> equippableItems = new List<GameObject>();


    private List<GameObject> equippedItems = new List<GameObject>();
    public InventorySystem SecondaryInvetroySystem => secondaryInvetroySystem;

    public static UnityAction<InventorySystem> OnPlayerEquipmentDisplayRequested;
    protected override void Awake()
    {
        base.Awake();

        secondaryInvetroySystem = new InventorySystem(secondaryInventroySize);
    }
    void Update()
    {
        
    }


    public void EnableEquipment(GameObject parent )
    {
        if (secondaryInventroySize == 0 && equippedItems.Count != 0)
        {
            return;
        }
        for (int i = 0; i < secondaryInvetroySystem.slots.Count; i++)
        {
            if (secondaryInvetroySystem.slots[i].ItemData != null)
            {

                if (secondaryInvetroySystem.slots[i].ItemData.equippableItemIndex == 0 && secondaryInvetroySystem.slots[i].ItemData.equipped == false)
                {
                    secondaryInvetroySystem.slots[i].ItemData.equipped = true;
                    GameObject item = Instantiate(equippableItems[0],parent.transform);

                    equippedItems.Add(item);    
                  

        
                }

                
            }

        }
    }

    public void DisableEquipment(  )
    {
      
        
        if (secondaryInvetroySystem.slots[0] == null && equippedItems.Count == 0)
        {
            return;
        }
        else if (secondaryInvetroySystem.slots[0] == null && equippedItems.Count!=0)
        {
            Destroy(equippedItems[0]);
            return;
        }
        else if (secondaryInvetroySystem.slots[0] != null && equippedItems.Count != 0)
        {
            
        }
       
       
    }

    public void DisableSlot(int index)
    {

        Destroy(equippedItems[index]);
       
    }


}
