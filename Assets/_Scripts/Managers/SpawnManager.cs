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

    public Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    private int count;
    private List<GameObject> entites;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        factory = GetComponent<IFactory>();
        spawnTime = timeBetweenSpawns;
        entites = new List<GameObject>(numberToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= numberToSpawn) return;
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float NTime = animatorStateInfo.normalizedTime;
        if (NTime <= 1.0f) return;

        if (spawnTime <= 0)
        {
            GameObject go =factory.Produce();
            go.GetComponent<Entity>().onDeathAction += removeEntity;
            entites.Add(go);
            spawnTime = timeBetweenSpawns;
            count++;
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void removeEntity(GameObject go)
    {
        entites.Remove(go);
    }

    public int getMaxEntites()
    {
        return numberToSpawn;
    }

    public int getCurrentEntityNum()
    {
        return entites.Count;
    }
}
