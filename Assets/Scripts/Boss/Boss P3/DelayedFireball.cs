using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFireball : MonoBehaviour
{
    [SerializeField] BossFireBall fireball;

    // Start is called before the first frame update
    void Start()
    {
        fireball.enabled = false;
        StartCoroutine(delayedAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delayedAttack()
    {
        yield return new WaitForSeconds(3f);
        fireball.enabled = true;
    }
}
