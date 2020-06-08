using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Spawner : MonoBehaviour
{
    ObjectPooling ObjectPooler;

    [SerializeField]
    private float spawnDelay = 10f;

    [SerializeField]
    private float LowerBound = 20f;

    [SerializeField]
    private float UpperBound = 30f;
    // Start is called before the first frame update

    private void Start()
    {
        ObjectPooler = ObjectPooling.Instance;
    }


    private void FixedUpdate()
    {
        Vector3 position = new Vector3(Random.Range(LowerBound, UpperBound), Random.Range(LowerBound, UpperBound), Random.Range(LowerBound, UpperBound));
        GameObject prefab = ObjectPooler.SpawnfromPool("Cube", position, transform.rotation);
        
    }
}
