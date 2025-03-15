using UnityEngine;

public class Movement_01 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0F;

    [SerializeField]
    private float moveDistance = 5.0F;

    private bool isMovingUp = true;

    void Update()
    {
        if (isMovingUp)
        {
            if (transform.position.y < moveDistance)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                isMovingUp = false;
            }
        }
        else
        {
            if (transform.position.y > 0)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
            else
            {
                isMovingUp = true;
            }
        }
    }
}