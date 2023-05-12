using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaCheck : MonoBehaviour
{
    [HideInInspector] public float CheckTime = 0f;

    private float nextTurn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AreaCheck();
    }
    
    private void AreaCheck()
    {
        if(Time.time > nextTurn)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,0);

            nextTurn = Time.time + CheckTime;
        }
    }

}
