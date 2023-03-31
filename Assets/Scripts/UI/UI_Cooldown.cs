using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Cooldown : MonoBehaviour
{
     Controll_Script _Controll;

    public float _CurrentEnergy;
    public float _MaxEnergy = 150;
    public float dValue = 5; //deduction value
    private int energy;
   


    public void Start()
    {
        _CurrentEnergy = _MaxEnergy; // makes the current energy start with full energy
    }


     void Update() // Test the energy decreaseing is working
    {

        if (energy == 1)
        {
            DecreseEnergy();
            print(_CurrentEnergy);
        }
        
    }

    public void DecreseEnergy()
    {
        if (_CurrentEnergy > 0)
        {

                _CurrentEnergy -= dValue * Time.deltaTime;
                print(_CurrentEnergy);
                Console.Clear();
                
        }

        else if (_CurrentEnergy < 0)
        {
            _CurrentEnergy = 0;
        }

        if (_CurrentEnergy > 150)
        {
            _CurrentEnergy = 150;
        }
    }

    public int IfDreamForm(int check)
    {
        if (check == 1)
        {
            energy = 1;
        }

         if (check == 2)
        {
            energy = 2;
        }

        return energy;
    }


}
