using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestInventory : Holder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public UnityAction<IInteractable> OnInteractComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact(Interactor interactor, out bool interactSucessful)
    {
        OnDynamicInvetoryDisplayRequested?.Invoke(inventorySystem);
        interactSucessful = true; 
    }

    public void EndInteraction()
    {

    }
}
