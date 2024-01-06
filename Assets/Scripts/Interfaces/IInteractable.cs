using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IInteractable 
{
   public UnityAction<IInteractable> OnInteractComplete { get; set; }
    public void Interact(Interactor interactor, out bool interactSucessful);
}
