using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeEffect : MonoBehaviour
{
    [SerializeField] private EnemyMeleeAttack _attackCheck;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] float speed = 10f;
    [SerializeField] float Timer = 0f;
    [SerializeField] float DestroyTimer = 0;
    public Transform enemyTransform;
    bool isShooting = false;

    [SerializeField] bool IsSpawned;

    [SerializeField] private Animator _anim;

    [SerializeField] private LayerMask _layer;

    // Start is called before the first frame update
    void Start()
    {
        IsSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_attackCheck.IsAttacking == true && IsSpawned == false)
        {
            Shoot();
            IsSpawned = true;
        }

        if(_attackCheck.IsCharging)
        {
            IsSpawned = false;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        bullet.transform.localScale = new Vector3(Mathf.Sign(enemyTransform.localScale.x), 1, 1);

        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Mathf.Sign(enemyTransform.localScale.x);

        Destroy(bullet, DestroyTimer);
    }

    private IEnumerator potato()
    {
        isShooting = true;
        //Shoot();
        yield return new WaitForSeconds(Timer);
        isShooting = false;
    }
}
