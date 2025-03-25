using UnityEngine;

public class AmmoBoxPickup : MonoBehaviour
{
    [Header("Ammo Box Settings")]
    [SerializeField] private int ammoAmount = 15;
    [SerializeField] private float rotationSpeed = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameCharacter player = other.GetComponent<GameCharacter>();

            if (player != null)
            {
                player.AddAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}