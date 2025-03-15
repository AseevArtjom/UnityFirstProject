using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private int lifeLevel = 100;
    public int ReactToHit(int damage)
    {
        lifeLevel -= damage;
        if (lifeLevel <= 0)
        {
            StartCoroutine(Die());
        }

        return lifeLevel;
    }

    public bool IsAlive()
    {
        return lifeLevel > 0;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75.0F,0.0F,0.0F);
        this.transform.Translate(0,-2,0);
        yield return new WaitForSeconds(1.5F);
        Destroy(this.gameObject);
    }
}
