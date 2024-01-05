using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Holder : MonoBehaviour
{
    [SerializeField] private int invetorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InvetorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInvetoryDisplayRequested;


    private void Awake()
    {
        inventorySystem = new InventorySystem(invetorySize);
    }
}
