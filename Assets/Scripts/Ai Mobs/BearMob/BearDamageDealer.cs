using UnityEngine;

public class BearDamageDealer : MonoBehaviour
{
    [SerializeField] private float clawDamage = 1f; // The damage dealt by the bear's claw
    private bool canDealDamage = false; // Flag to control when the claw can deal damage

    private void OnTriggerEnter(Collider other)
    {
        // Check if the claw is allowed to deal damage and if it has collided with the player
        if (canDealDamage && other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(clawDamage);

                // Trigger hit VFX at the point of collision
                // Assuming you have a method like HitVFX(Vector3 position) in your HealthSystem
                playerHealth.HitVFX(other.ClosestPointOnBounds(this.transform.position));
            }

            canDealDamage = false; // Reset flag to avoid multiple hits in one attack
        }
    }

    // Method to enable damage dealing, called at the start of the bear's attack animation
    public void StartDealDamage()
    {
        canDealDamage = true;
    }

    // Method to disable damage dealing, called at the end of the bear's attack animation
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
}
