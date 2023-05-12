using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEnemy : MonoBehaviour
{
    [SerializeField] public Transform[] SpawnpointList;
    [SerializeField] public GameObject[] SummonList;
    private GameObject SummonObject;
    private Transform SummonSpawnpoint;

    [SerializeField] public float SummonDelay = 0f;

    public int spawnPointIndex;
    public int enemyIndex;

    float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SummonEnemy();
    }

    private void RandomObjectAndPos()
    {
        enemyIndex = Random.Range(0, SummonList.Length);
        SummonObject = SummonList[enemyIndex];

        spawnPointIndex = Random.Range(0, SummonList.Length);
        SummonSpawnpoint = SpawnpointList[spawnPointIndex];
    }

    private void SummonEnemy()
    {
        if (Time.time >= nextSpawn)
        {
            RandomObjectAndPos();

            Instantiate(SummonObject, SummonSpawnpoint.position, Quaternion.identity);
            nextSpawn = Time.time + SummonDelay;
        }
    }
}
