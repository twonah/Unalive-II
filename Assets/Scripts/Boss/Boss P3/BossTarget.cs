using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossTarget : MonoBehaviour
{
    [SerializeField] AIDestinationSetter destinationAI;
    [SerializeField] Controll_Script cs;
    [SerializeField] BossHP_P3 B_HP;
    [SerializeField] BossMeleeAttack B_meleeAttack;
    [SerializeField] BossSummonLaser B_Summonlaser;
    [SerializeField] CallLaser B_CallLaser;
    [SerializeField] GameObject FireballSpawner;

    // Start is called before the first frame update
    void Start()
    {
        destinationAI.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!B_HP.isStunned)
        {
            if (cs.isDreamform)
            {
                destinationAI.target = GameObject.FindGameObjectWithTag("DreamForm").transform;
            }
            else
            {
                destinationAI.target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        else
        {
            destinationAI.target = GameObject.FindGameObjectWithTag("Boss").transform;
            B_meleeAttack.enabled = false;
            B_Summonlaser.enabled = false;
            B_CallLaser.enabled = false;
            FireballSpawner.SetActive(false);
        }
    }
}
