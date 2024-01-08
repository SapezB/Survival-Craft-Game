using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingHolder : EquipmentHolder
{
    [SerializeField] protected int fourthInventroySize;
    [SerializeField] protected InventorySystem fourthInventorySystem;

    
    public InventorySystem foruthInventroySystem => fourthInventorySystem;

    public static UnityAction<InventorySystem> foruthInvetnroySystem;
    public static UnityAction<InventorySystem> OnPlayerCraftingDisplayRequested;
    protected override void Awake()
    {
        base.Awake();

        fourthInventorySystem = new InventorySystem(fourthInventroySize);
    }
    void Update()
    {

    }


   

}
