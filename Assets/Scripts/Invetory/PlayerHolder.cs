using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerHolder : EquipmentHolder
{
  
    [SerializeField] protected int thirdInventroySize;


    [SerializeField] protected InventorySystem thirdInvetroySystem;



    public InventorySystem ThirdInvetroySystem => thirdInvetroySystem;




    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    
    protected override void Awake()
    {
        base.Awake();

 
        thirdInvetroySystem = new InventorySystem(thirdInventroySize);

    }
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            OnPlayerEquipmentDisplayRequested?.Invoke(secondaryInvetroySystem);
            OnPlayerBackpackDisplayRequested?.Invoke(thirdInvetroySystem);
        }
    }


    public bool AddToInventory(ItemData data ,int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount)) return true;

        else if(thirdInvetroySystem.AddToInventory(data, amount)) { return true; }


        return false;
    }
}
