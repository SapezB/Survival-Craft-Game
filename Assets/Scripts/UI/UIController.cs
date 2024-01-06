using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public DynamicInvetoryDisplay invetoryDisplay;

    private void Awake()
    {
        invetoryDisplay.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Holder.OnDynamicInvetoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        Holder.OnDynamicInvetoryDisplayRequested -= DisplayInventory;
    }



    void Update()
    {

        if (invetoryDisplay.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) invetoryDisplay.gameObject.SetActive(false);
    }



    void DisplayInventory(InventorySystem invToDisplay)
    {
        invetoryDisplay.gameObject.SetActive(true);
        invetoryDisplay.RefreshDynamicInventory(invToDisplay);
    }
}
