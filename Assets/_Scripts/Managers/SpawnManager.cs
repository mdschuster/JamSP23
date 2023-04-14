/*
Copyright 2023 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
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
        if (entites.Count == 0)
        {
            GameManager.instance().GameOver();
        }
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
