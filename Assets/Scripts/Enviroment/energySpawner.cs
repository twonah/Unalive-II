using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energySpawner : MonoBehaviour
{
    [SerializeField] GameObject energy;
    [SerializeField] float spawnDelay;

    [SerializeField] int spawnedCount;
    [SerializeField] int maxCount;

    [SerializeField] float spawnRadius;

    [SerializeField] bool isSpawning;

    [SerializeField] bool detectPlayer;
    [SerializeField] GameObject spawner;
    //[SerializeField] Collider2D rangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = false;
        isSpawning = false;
        spawnedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedCount <= maxCount && isSpawning == false && detectPlayer)
        {
            StartCoroutine(delayedSpawn());
        }

        //if(rangeCollider.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("Player Detected");
        //    detectPlayer = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DreamForm"))
        {
            Debug.Log("Player Detected");
            detectPlayer = true;
        }
    }

    IEnumerator delayedSpawn()
    {
        isSpawning = true;
        spawnedCount += 1;
        Vector2 spawnPos = (Vector2)transform.position + new Vector2(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
        Instantiate(energy, spawnPos, Quaternion.identity);
        //Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        isSpawning = false;
    }
}
