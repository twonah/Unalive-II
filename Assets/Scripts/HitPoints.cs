using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints;
    [SerializeField] public float _CurrentHitPoints;

    [SerializeField] private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentHitPoints = _maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("TakingDamage", 0);
    }

    public void TakeDamage(float damage)
    {
        _CurrentHitPoints -= damage;
        _anim.SetFloat("TakingDamage", 1);

        if(_CurrentHitPoints <= 0)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
