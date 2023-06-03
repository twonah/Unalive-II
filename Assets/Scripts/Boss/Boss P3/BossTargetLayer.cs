using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargetLayer : MonoBehaviour
{
    [SerializeField] Controll_Script cs;
    [SerializeField] BossMeleeAttack bossMelee;
    [SerializeField] LayerMask _dreamformLayer;
    [SerializeField] LayerMask _playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cs.isDreamform)
        {
            bossMelee._TargetLayer = _dreamformLayer;
        }

        if(!cs.isDreamform)
        {
            bossMelee._TargetLayer = _playerLayer;
        }
    }
}
