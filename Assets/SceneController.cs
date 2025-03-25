using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int maxTargets = 5;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject healthPackPrefab;
    [SerializeField] private GameObject ammoBoxPrefab;

    private GameObject[] targets;
    private float nextHealthPackTime = 0f;
    private float healthPackSpawnInterval = 10f;

    private float nextAmmoBoxTime = 0f;
    private float ammoBoxSpawnInterval = 5f;

    private float minX, maxX, minZ, maxZ;
    private float targetSpawnHeight = 2.0F;
    private float healthPackHeight;
    private float ammoBoxHeight;

    void Start()
    {
        targets = new GameObject[maxTargets];

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            Vector3 size = collider.size;
            Vector3 center = collider.center;

            minX = transform.position.x - size.x / 2 + center.x;
            maxX = transform.position.x + size.x / 2 + center.x;
            minZ = transform.position.z - size.z / 2 + center.z;
            maxZ = transform.position.z + size.z / 2 + center.z;

            healthPackHeight = transform.position.y + size.y / 2 + 0.5f;
            ammoBoxHeight = transform.position.y + size.y / 2 + 0.5f;
        }
        else
        {
            Debug.LogError("BoxCollider не найден на объекте " + gameObject.name);
        }
        
        for (int i = 0; i < maxTargets; i++)
        {
            targets[i] = Instantiate(targetPrefab);
            targets[i].transform.position = GetRandomPosition();
            targets[i].transform.Rotate(0, Random.Range(0, 360), 0);
        }
    }

    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                targets[i] = Instantiate(targetPrefab);
                targets[i].transform.position = GetRandomPosition();
                targets[i].transform.Rotate(0, Random.Range(0, 360), 0);
            }
        }

        if (Time.time >= nextHealthPackTime)
        {
            SpawnHealthPack();
            nextHealthPackTime = Time.time + healthPackSpawnInterval;
        }

        if (Time.time >= nextAmmoBoxTime)
        {
            SpawnAmmoBox();
            nextAmmoBoxTime = Time.time + ammoBoxSpawnInterval;
        }
    }

    void SpawnAmmoBox()
    {
        Vector3 randomPosition = GetRandomPosition();
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(ammoBoxPrefab, randomPosition, rotation);
    }
    
    void SpawnHealthPack()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(healthPackPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, healthPackHeight, randomZ);
    }
}
