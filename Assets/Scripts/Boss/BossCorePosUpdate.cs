using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCorePosUpdate : MonoBehaviour
{
    [SerializeField] private GameObject Core;
    [SerializeField] private GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Core.transform.position = Boss.transform.position;
        Core.transform.localScale = new Vector2(Boss.transform.localScale.x, Core.transform.localScale.y);
    }
}
