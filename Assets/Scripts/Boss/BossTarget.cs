using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossTarget : MonoBehaviour
{
    [SerializeField] AIDestinationSetter destinationAI;
    [SerializeField] Controll_Script cs;

    // Start is called before the first frame update
    void Start()
    {
        destinationAI.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cs.isDreamform)
        {
            destinationAI.target = GameObject.FindGameObjectWithTag("DreamForm").transform;
        }
        else
        {
            destinationAI.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
