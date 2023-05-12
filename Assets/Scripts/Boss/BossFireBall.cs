using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    [SerializeField] public GameObject fireBall;
    [SerializeField] private Transform HandPos;
    [SerializeField] public float spawnDelay;
    //[SerializeField] public bool FireBallSpawn;
    Vector3 randomRange;

    float nextSpawn;
    float setFalse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SummonFireBall();
    }

    private void FixedUpdate()
    {

    }

    private void SummonFireBall()
    {
        if(Time.time >= nextSpawn)
        {
            //FireBallSpawn = true;
            //randomRange = (Vector2)transform.position + new Vector2((Random.Range(-3f, 3f)), (Random.Range(-3f, 3f)));
            Instantiate(fireBall, HandPos.position, Quaternion.identity);
            nextSpawn = Time.time + spawnDelay;
            setFalse = nextSpawn - 0.5f;
        }
        else
        {
            //FireBallSpawn = false;
        }
    }


}
