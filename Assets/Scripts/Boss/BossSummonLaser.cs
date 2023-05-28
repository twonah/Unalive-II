using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonLaser : MonoBehaviour
{
    [SerializeField] private GameObject[] _laser;
    [SerializeField] private int _activateAmount;
    [SerializeField] public bool _IsSummonLaser;

    private int index;
    private int num;

    public bool SummonDone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActivateLaser();
    }

    private void OnDisable()
    {
        index = 0;
        SummonDone = false;
    }

    private void ActivateLaser()
    {
        while(index < _activateAmount)
        {
            num = Random.Range(0, _laser.Length);
            _laser[num].SetActive(true);
            index++;
            
        }

        if(_laser[num].activeSelf)
        {
            _IsSummonLaser = true;
            //SummonDone = false;
        }
        else
        {
            _IsSummonLaser = false;
            SummonDone = true;
        }
    }
}
