using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1;


    public bool IsInteracting{ get; private set; }


    private void Update()
    {
       var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interatable = colliders[i].GetComponent<IInteractable>();

                if (interatable != null) StartInteraction(interatable);
            }
        }
       
    }

    void StartInteraction(IInteractable interatable)
    {
        interatable.Interact(this, out bool interactSucessful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;
    }

}
