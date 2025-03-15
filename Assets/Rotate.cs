using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float Speed_X = 3.0f;

    [SerializeField]
    private float Speed_Y = 3.0f;

    [SerializeField]
    private float Speed_Z = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Speed_Y += 0.1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Speed_Y -= 0.1F;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Speed_Y = 0f;
        }
        transform.Rotate(Speed_X,Speed_Y,Speed_Z);
    }
}
