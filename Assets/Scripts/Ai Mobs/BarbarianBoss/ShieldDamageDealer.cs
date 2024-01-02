using UnityEngine;
using System.Collections;

public class ShieldDamageDealer : MonoBehaviour
{
    [SerializeField] private float shieldDamage = 1f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.5f;
    private bool canDealDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            CharacterController playerController = other.GetComponent<CharacterController>();

            if (playerHealth != null && playerController != null)
            {
                playerHealth.TakeDamage(shieldDamage);

                // Start coroutine for knockback
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                StartCoroutine(KnockbackPlayer(playerController, knockbackDirection, knockbackDuration));

                canDealDamage = false; // Reset to avoid multiple hits in one attack
            }
        }
    }

    private IEnumerator KnockbackPlayer(CharacterController controller, Vector3 direction, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            // Apply knockback
            controller.Move(direction * knockbackForce * Time.deltaTime);
            yield return null;
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }
}
