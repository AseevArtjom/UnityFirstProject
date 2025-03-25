using UnityEngine;

public class TankTurretControl : MonoBehaviour
{
    [SerializeField] private float turretRotationSpeed = 9.0f;
    
    [SerializeField] private float gunRotationSpeed = 9.0f;
    [SerializeField] private float minGunAngle = -5.0f;
    [SerializeField] private float maxGunAngle = 30.0f;
    
    private float currentGunAngle = 0.0f;

    [SerializeField] private Transform turret;
    [SerializeField] private Transform gun;

    void Start()
    {
        if (turret == null || gun == null)
        {
            Debug.LogError("Башня или пушка не назначены!");
        }
    }

    void Update()
    {
        float turretRotation = Input.GetAxis("Mouse X") * turretRotationSpeed;
        turret.Rotate(0, 0, turretRotation);
        
        float gunRotation = -Input.GetAxis("Mouse Y") * gunRotationSpeed;
        currentGunAngle += gunRotation;
        currentGunAngle = Mathf.Clamp(currentGunAngle, minGunAngle, maxGunAngle);

        gun.localRotation = Quaternion.Euler(currentGunAngle, 0, 0);
    }
}