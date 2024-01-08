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
    public List<GameObject> EquippedItems => equippedItems;


    public static UnityAction<InventorySystem> OnPlayerEquipmentDisplayRequested;
    protected override void Awake()
    {
        base.Awake();

        secondaryInvetroySystem = new InventorySystem(secondaryInventroySize);
    }
    void Update()
    {

    }


    public void EnableEquipment(GameObject parent)
    {
        if (secondaryInvetroySystem.slots[0].ItemData!= null)
        {
            if (secondaryInvetroySystem.slots[0].ItemData.equippableItemIndex == 10 && secondaryInvetroySystem.slots[0].ItemData.equipped == false)
            {
              
                GameObject item = Instantiate(equippableItems[0], parent.transform);
                secondaryInvetroySystem.slots[0].ItemData.equipped = true;

                equippedItems.Add(item);
            }

        }








    }
    


 }