using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    [HideInInspector] public Transform SummonSpawnpoint;
    [HideInInspector] public GameObject[] SummonList;
    [SerializeField] public GameObject SummonObject;

    [HideInInspector] public float SummonDelay = 0f;

    [HideInInspector] public Transform MageTransform;

    [SerializeField] public bool isSummon;
    [SerializeField] public bool RandomSummon;

    public int index;

    private float beforeSpawn = 0f;
    private float nextSpawn = 0f;
    private float setFalse = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RandomObject();
    }

    private void FixedUpdate()
    {

    }

    private void RandomObject()
    {
        if(Time.time > nextSpawn || isSummon == false )
        {
            if(!RandomSummon)
            {
                index = Random.Range(0, SummonList.Length);
                SummonObject = SummonList[index];

                RandomSummon = true;
            }
            
        }
    }

    public void Summon()   //Use in animation
    {
        GameObject bullet = Instantiate(SummonObject, SummonSpawnpoint.position, SummonSpawnpoint.rotation);

        nextSpawn = Time.time + SummonDelay;

        isSummon = true;

        setFalse = nextSpawn - 2f;

        if (setFalse <= Time.time)
        {
            isSummon = false;
            RandomSummon = false;
        }

    }
}
