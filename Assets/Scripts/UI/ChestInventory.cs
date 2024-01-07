using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestInventory : Holder, IInteractable
{

    public UnityAction<IInteractable> OnInteractComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSucessful)
    {
        OnDynamicInvetoryDisplayRequested?.Invoke(primaryInventorySystem);
        interactSucessful = true; 
    }

    public void EndInteraction()
    {

    }
}
