using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [HideInInspector] public Transform BulletSpawnpoint;
    [HideInInspector] public GameObject BulletPrefab;

    [HideInInspector] public float BulletSpeed = 0f;
    [HideInInspector] public float SpawnDelay = 0f;

    [HideInInspector] public Transform gunnerTransform;

    [SerializeField] public bool isShoot;

    private float nextSpawn = 0f;
    private float setFalse = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time > nextSpawn)
        {
            //Shoot & Direction
            //GameObject bullet = Instantiate(BulletPrefab, BulletSpawnpoint.position, BulletSpawnpoint.rotation);
            //bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed * Mathf.Sign(gunnerTransform.localScale.x);

            nextSpawn = Time.time + SpawnDelay;

            isShoot = true;

            setFalse = nextSpawn - 2f;
            //Debug.Log(nextSpawn +" | "+ setFalse);
        }
        if (setFalse <= Time.time)
        {
            isShoot = false;
            //Debug.Log("TEst");
        }

    }

    public void BulletOut()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawnpoint.position, BulletSpawnpoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed * Mathf.Sign(gunnerTransform.localScale.x);
    }
}
