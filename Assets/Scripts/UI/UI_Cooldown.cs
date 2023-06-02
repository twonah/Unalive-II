using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Cooldown : MonoBehaviour
{
    public Controll_Script _Controll;

    public float _CurrentEnergy;
    public float _MaxEnergy = 150f;
    public float dValue = 5f; //deduction value
    private int energy;
   


    public void Start()
    {
        _CurrentEnergy = _MaxEnergy; // makes the current energy start with full energy
        _Controll = FindObjectOfType<Controll_Script>();
    }


     void Update() // Test the energy decreaseing is working
    {
        if (energy == 1)
        {
            DecreseEnergy();
            //print(_CurrentEnergy);
        }
        
    }

    public void DecreseEnergy()
    {
        if (_CurrentEnergy > 0 && _Controll.isDreamform)
        {
            _CurrentEnergy -= dValue * Time.deltaTime;
            //Debug.Log("Draining Energy");
            //print(_CurrentEnergy);
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
