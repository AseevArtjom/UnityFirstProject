using System.Collections;
using UnityEngine;

public class TankGunShooter : MonoBehaviour
{
    [SerializeField] private GUIStyle style = new GUIStyle();
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Color crosshairColor = Color.red;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private float gunLength = 2.0f;

    private Camera _camera;

    void Start()
    {
        _camera = playerCamera != null ? playerCamera : GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (gunTransform == null)
        {
            Debug.LogError("Gun Transform (Main_barre) не привязан!");
        }
    }

    void Update()
    {
        if (gunTransform != null)
        {
            Vector3 gunEndPosition = gunTransform.position + gunTransform.forward * gunLength;

            Vector3 gunDirection = gunTransform.forward;
            
            Ray ray = new Ray(gunEndPosition, gunDirection);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                    if (target != null)
                    {
                        target.ReactToHit(20);
                    }
                    else
                    {
                        StartCoroutine(SphereIndicator(hit.point, 2.0F)); 
                    }

                    Debug.Log("Попал в объект: " + hitObject.name);
                }
                else
                {
                    Debug.Log("Луч не попал в объекты.");
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 position, float radius)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(radius, radius, radius);
        sphere.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(1.05F);
        Destroy(sphere);
    }

    private void OnGUI()
    {
        style.normal.textColor = crosshairColor; 

        int size = 16;
        float posX = Screen.width * 0.5F - size * 0.5F;
        float posY = Screen.height * 0.5F - size * 0.5F;
        
        GUI.Label(new Rect(posX, posY, size, size), "+", style);
    }
}
