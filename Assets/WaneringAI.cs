using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(ReactiveTarget))]
public class WaneringAI : MonoBehaviour
{
    [SerializeField] private float speed = 3.0F;
    [SerializeField] private float obstacleRange = 5.0F;
    void Start()
    {
        
    }

    void Update()
    {
        ReactiveTarget obj = GetComponent<ReactiveTarget>();
        if (obj.IsUnityNull() || !obj.IsAlive()) return;
        
        transform.Translate(0,0,speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray,0.75F,out hit))
        {
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110.0F, 110.0F);
                transform.Rotate(0,angle,0);
            }
        }
    }
}
