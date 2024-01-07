using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class PickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;

    public ItemData ItemData;

    private SphereCollider SphereCollider;

    private void Awake()
    {
        SphereCollider = GetComponent<SphereCollider>();
        SphereCollider.isTrigger = true;
        SphereCollider.radius = PickUpRadius;

    }


    private void OnTriggerEnter(Collider other)
    {
        var invetory = other.transform.GetComponent<PlayerHolder>();

        if (!invetory) return;
        if (invetory.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
