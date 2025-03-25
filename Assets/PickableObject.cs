using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = false; 
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}