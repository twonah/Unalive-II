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

    public float nextSummon = 0f;

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
        if(/*Time.time > nextSummon || */isSummon == true)
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
    }

    public void DoneSummon()
    {
        isSummon = false;
        RandomSummon = false;
        nextSummon = Time.time + SummonDelay;
    }
}
