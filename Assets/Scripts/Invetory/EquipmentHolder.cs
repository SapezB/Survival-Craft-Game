using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EquipmentHolder : Holder
{
    [SerializeField] protected int secondaryInventroySize;
    [SerializeField] protected InventorySystem secondaryInvetroySystem;

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


  
}
