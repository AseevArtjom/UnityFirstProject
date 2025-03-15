using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control script / FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField] private float speed = 6.0F;
    [SerializeField] private float gravity = 9.8F;

    private CharacterController _characterController;
    private Vector3 _velocity;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(horizontal, _velocity.y, vertical);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        
        if (!_characterController.isGrounded)
        {
            _velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0f;
        }
        
        movement = transform.TransformDirection(movement);
        
        _characterController.Move(movement);
    }
}
