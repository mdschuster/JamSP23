using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour, IFactory
{
    [Header("Entity Spawn Information")]
    [SerializeField] private GameObject EntityToSpawn;
    [SerializeField] private GameObject SpawnLocation;
    [SerializeField] private GameObject EntityParent;
    
    public GameObject Produce()
    {
        return Instantiate(EntityToSpawn, SpawnLocation.transform.position, Quaternion.identity, EntityParent.transform);
    }

    

}
