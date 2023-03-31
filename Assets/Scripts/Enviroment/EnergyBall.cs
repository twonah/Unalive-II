using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{

    [SerializeField] UI_Cooldown _CoolUP;

    int EnergyGet = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnergyBall")
        {
            _CoolUP._CurrentEnergy += EnergyGet;
            Destroy(collision.gameObject);
        }
    }

}
