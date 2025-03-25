using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(CharacterController))]
public class GameCharacter : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float gravity = 9.8f;

    [Header("Mouse Look Settings")]
    [SerializeField] private float sensivityHorz = 9.0f;
    [SerializeField] private float sensivityVert = 9.0f;
    [SerializeField] private float minVertAngle = -45.0f;
    [SerializeField] private float maxVertAngle = 45.0f;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private Slider healthSlider;
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 30;

    private int currentAmmo;

    public int getCurrentAmmo()
    {
        return currentAmmo;
    }

    private CharacterController characterController;
    private Vector3 velocity;
    private float rotationX = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHealth = 40;
        currentAmmo = maxAmmo;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (ammoText != null)
        {
            UpdateAmmoUI();
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        UpdateHealthUI();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;
        
        Vector3 movement = new Vector3(horizontal, velocity.y, vertical);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        movement *= Time.deltaTime;

        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }

        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
        rotationX = Mathf.Clamp(rotationX, minVertAngle, maxVertAngle);

        float rotationY = transform.localEulerAngles.y + (Input.GetAxis("Mouse X") * sensivityHorz);
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Character has died");
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
    
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoUI();
    }


    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo.ToString();
        }
    }
    
    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }
    
    public void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI(); 
    }
}
