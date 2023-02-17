using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints;
    [SerializeField] public float _CurrentHitPoints;

    [SerializeField] public bool _IsTakingDamage;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentHitPoints = _maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        _CurrentHitPoints -= damage;
        _IsTakingDamage = true;


        if(_CurrentHitPoints <= 0)
        {
            //Die
        }
 
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
