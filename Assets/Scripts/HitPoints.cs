using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints;
    [SerializeField] private float _currentHitPoints;

    [SerializeField] private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _currentHitPoints = _maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("TakingDamage", 0);
    }

    public void TakeDamage(float damage)
    {
        _currentHitPoints -= damage;
        _anim.SetFloat("TakingDamage", 1);

        if(_currentHitPoints <= 0)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
