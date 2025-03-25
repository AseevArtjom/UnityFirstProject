using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameCharacter player = other.GetComponent<GameCharacter>();

            if (player != null)
            {
                player.Heal(healthAmount);
                Destroy(gameObject);
            }
        }
    }

}