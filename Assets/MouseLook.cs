using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
    
    [SerializeField] private float sensivityHorz = 9.0F;
    [SerializeField] private float sensivityVert = 9.0F;
    [SerializeField] private float minVertAngle = -45.0F;
    [SerializeField] private float maxVertAngle = 45.0F;

    private float _rotationX = 0.0F;
    
    [SerializeField]
    private RotationAxes axes = RotationAxes.MouseXAndY;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if(rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0,Input.GetAxis("Mouse X") * sensivityHorz,0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVertAngle, maxVertAngle);

            float _rotationY = transform.localEulerAngles.y;
            
            transform.localEulerAngles = new Vector3(_rotationX,_rotationY,0.0F);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVertAngle, maxVertAngle);

            float delta = Input.GetAxis("Mouse X") * sensivityHorz;
            float _rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX,_rotationY,0.0F);
        }
    }
}
