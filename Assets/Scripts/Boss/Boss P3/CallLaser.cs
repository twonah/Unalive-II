using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLaser : MonoBehaviour
{
    [SerializeField] BossSummonLaser laser;
    [SerializeField] bool summonLaser;
    [SerializeField] float summonInterval;

    // Start is called before the first frame update
    void Start()
    {
        summonLaser = true;
        laser.enabled = false;
        StartCoroutine(startSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if(summonLaser == false)
        {
            StartCoroutine(sequenceAttack());
        }
    }

    void laserAttack()
    {
        laser.enabled = true;
        if(laser.SummonDone)
        {
            summonLaser = false;
            laser.enabled = false;
        }
    }

    IEnumerator sequenceAttack()
    {
        summonLaser = true;
        laserAttack();
        summonLaser = false;
        yield return new WaitForSeconds(summonInterval);
    }

    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(summonInterval);
        laserAttack();
        summonLaser = false;
        yield return new WaitForSeconds(summonInterval);
    }
}
