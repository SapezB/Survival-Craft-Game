using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public DynamicInvetoryDisplay chestPanel;
    public DynamicInvetoryDisplay playerBackpackPanel;
    public DynamicInvetoryDisplay playerEquipmentPanel;
    public DynamicInvetoryDisplay playerCraftingPanel;
    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
        playerEquipmentPanel.gameObject.SetActive(false);
        playerCraftingPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Holder.OnDynamicInvetoryDisplayRequested += DisplayInventory;
        PlayerHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
        EquipmentHolder.OnPlayerEquipmentDisplayRequested += DisplayPlayerEquipment;
        CraftingHolder.OnPlayerCraftingDisplayRequested += DisplayPlayerCrafting;
    }

    private void OnDisable()
    {
        Holder.OnDynamicInvetoryDisplayRequested -= DisplayInventory;
        PlayerHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
        EquipmentHolder.OnPlayerEquipmentDisplayRequested -= DisplayPlayerEquipment;
        CraftingHolder.OnPlayerCraftingDisplayRequested -= DisplayPlayerCrafting;
    }



    void Update()
    {

        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) chestPanel.gameObject.SetActive(false);
        if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) playerBackpackPanel.gameObject.SetActive(false);
        if (playerEquipmentPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) playerEquipmentPanel.gameObject.SetActive(false);
        if (playerCraftingPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) playerCraftingPanel.gameObject.SetActive(false);
    }



    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
    void DisplayPlayerEquipment(InventorySystem invToDisplay)
    {
        playerEquipmentPanel.gameObject.SetActive(true);
        playerEquipmentPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerCrafting(InventorySystem invToDisplay)
    {
        playerCraftingPanel.gameObject.SetActive(true);
        playerCraftingPanel.RefreshDynamicInventory(invToDisplay);
    }
}

