using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private static int maxTargets = 5;

    [SerializeField] private GameObject targetPrefab;
    private GameObject[] targets = new GameObject[maxTargets];
    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].IsUnityNull())
            {
                targets[i] = Instantiate(targetPrefab);
                targets[i].transform.position = new Vector3(Random.Range(-23.0F, 23.0F),2.5F,Random.Range(-23.0F,23.0F));
                targets[i].transform.Rotate(0,Random.Range(0,360),0);
            }
        }
    }
}
