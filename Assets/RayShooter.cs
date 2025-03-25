using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RayShoot : MonoBehaviour
{
    [SerializeField] private Camera characterCamera;
    [SerializeField] private GameCharacter gameCharacter;

    void Start()
    {
        if (characterCamera == null)
        {
            characterCamera = GetComponent<Camera>();
        }
        
        if (gameCharacter == null)
        {
            gameCharacter = GetComponent<GameCharacter>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameCharacter != null && gameCharacter.GetComponent<GameCharacter>().getCurrentAmmo() > 0)
            {
                gameCharacter.Shoot();
                Vector3 point = new Vector3(characterCamera.pixelWidth / 2.0F, characterCamera.pixelHeight / 2.0F, 0.0F);
                Ray ray = characterCamera.ScreenPointToRay(point);
                RaycastHit hit;

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
                        StartCoroutine(SphereIndicator(hit.point, 1.0F));
                    }
                }
            }
            else
            {
                Debug.Log("Cannot shoot, out of ammo!");
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 position, float radius)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(radius, radius, radius);

        yield return new WaitForSeconds(1.05F);
        Destroy(sphere);
    }
}
