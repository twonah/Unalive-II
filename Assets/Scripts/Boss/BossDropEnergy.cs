using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropEnergy : MonoBehaviour
{
    [SerializeField] public GameObject Energy;
    [SerializeField] public int EnergyAmount;
    [SerializeField] public Transform Spawnpoint;

    Vector2 randomRange;

    private int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnergyOrbs();
    }

    private void OnDisable()
    {
        index = 0;
    }

    private void SpawnEnergyOrbs()
    {
        while (index < EnergyAmount)
        {
            randomRange = (Vector2)Spawnpoint.position + new Vector2((Random.Range(-2f, 2f)), (Random.Range(-2f, 2f)));
            Instantiate(Energy, randomRange, Quaternion.identity);
            index++;
        }



    }
}
