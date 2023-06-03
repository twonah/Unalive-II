using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] HitPoints _DreamForm_HitP;
    [SerializeField] UI_Cooldown _CoolUP;

    int EnergyGet = 25;
    int DreamForm_revive = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnergyBall")
        {
            _CoolUP._CurrentEnergy += EnergyGet;

            _DreamForm_HitP._CurrentHitPoints += DreamForm_revive; //heals dreamform

            Destroy(collision.gameObject);
        }
    }


}
