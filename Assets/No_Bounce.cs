using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No_Bounce : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private HitPoints Hp;
    [SerializeField] private Controll_Script CS;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Hp._CurrentHitPoints < 0) //Rigid body is disabled when the player dies
        {
            Rb.simulated = false;
        }
        //else
        //{
        //    Rb.simulated = true;
        //}
    }

}
