using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MouseItemData : MonoBehaviour
{
    public Image Icon;
    public Image Frame;
    public TextMeshProUGUI ItemCount;
    public Slot InvetorySlot;

    private void Awake()
    {
        Icon.color = Color.clear;
        Frame.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(Slot slot)
    {
        InvetorySlot.AssignItem(slot);
        Icon.sprite = slot.ItemData.Icon;
        ItemCount.text = slot.StackSize.ToString();
        Icon.color = Color.white;
    }

    private void Update()
    {
        if (InvetorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
         
        }
    }

    public void ClearSlot()
    {
        InvetorySlot.ClearSlot();
        ItemCount.text = "";
        Icon.color = Color.clear;
        Icon.sprite = null;
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition,results);
        return results.Count > 0;
    }
}
