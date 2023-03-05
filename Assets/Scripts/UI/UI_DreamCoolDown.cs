using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_DreamCoolDown : MonoBehaviour
{
    public int _maxDreamJuice = 100;
    public int _currentDreamJuice;

    public UI_CooldownBar _coolbar; // calls for CooldownBar

    // Start is called before the first frame update
    void Start()
    {
        _currentDreamJuice =_maxDreamJuice; // Starts with max juice
        _coolbar.SetMaxJuice(_maxDreamJuice); // makes the UI_Cooldown bar value to max value
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentDreamJuice > 0)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Test(20);
            }
        }

        
    }


    void Test(int cooldownTest)
    {
        _currentDreamJuice -= cooldownTest;
    }

}
