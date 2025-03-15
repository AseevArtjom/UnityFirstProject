using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RayShoot : MonoBehaviour
{
    [SerializeField] private GUIStyle Style = new GUIStyle();
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2.0F, _camera.pixelHeight / 2.0F, 0.0F);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
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
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 position, float raduis)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;

        yield return new WaitForSeconds(1.05F);
        Destroy(sphere);
    }
    
    private void OnGUI()
    {
        int size = 16;
        float posX = Screen.width * 0.5F - size * 0.5F;
        float posY = Screen.height * 0.5F - size * 0.5F;
        GUI.Label(new Rect(posX,posY,size,size),"+");
    }
}
