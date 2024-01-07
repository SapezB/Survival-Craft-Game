using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Holder : MonoBehaviour
{
    [SerializeField] private int invetorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem PrimaryInvetorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInvetoryDisplayRequested;


    protected virtual void Awake()
    {
        primaryInventorySystem = new InventorySystem(invetorySize);
    }
}
