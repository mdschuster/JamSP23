using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private IFactory factory;
    private float spawnTime;

    [Header("Spawn Parameters")]
    [SerializeField] private int numberToSpawn;
    [SerializeField] private float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        factory = GetComponent<IFactory>();
        spawnTime = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0)
        {
            factory.Produce();
            spawnTime = timeBetweenSpawns;
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }
}
