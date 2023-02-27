using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HitPoint : MonoBehaviour
{

    [SerializeField] private HitPoints _dreamFormHitPoints;
    [SerializeField] private HitPoints _PhysicalFormHitPoints;

    [SerializeField] private int _numOfHeartsDreamForm;
    [SerializeField] private int _numOfHeartsPhysicalForm;

    [SerializeField] private Image[] _dreamFormHearts;
    [SerializeField] private Image[] _physicalFormHeart;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        PhysicalFormHitPointsUI();

        DreamFormHitPointsUI();
    }

    private void DreamFormHitPointsUI()
    {
        if (_dreamFormHitPoints._CurrentHitPoints > _numOfHeartsDreamForm)
        {
            _dreamFormHitPoints._CurrentHitPoints = _numOfHeartsDreamForm;
        }

        for (int i = 0; i < _dreamFormHearts.Length; i++)
        {
            if (i < _dreamFormHitPoints._CurrentHitPoints)
            {
                _dreamFormHearts[i].sprite = _fullHeart;
            }
            else
            {
                _dreamFormHearts[i].sprite = _emptyHeart;
            }

            if (i < _numOfHeartsDreamForm)
            {
                _dreamFormHearts[i].enabled = true;
            }
            else
            {
                _dreamFormHearts[i].enabled = false;
            }
        }
    }

    private void PhysicalFormHitPointsUI()
    {
        if (_PhysicalFormHitPoints._CurrentHitPoints > _numOfHeartsDreamForm)
        {
            _PhysicalFormHitPoints._CurrentHitPoints = _numOfHeartsDreamForm;
        }

        for (int i = 0; i < _physicalFormHeart.Length; i++)
        {
            if (i < _PhysicalFormHitPoints._CurrentHitPoints)
            {
                _physicalFormHeart[i].sprite = _fullHeart;
            }
            else
            {
                _physicalFormHeart[i].sprite = _emptyHeart;
            }

            if (i < _numOfHeartsDreamForm)
            {
                _physicalFormHeart[i].enabled = true;
            }
            else
            {
                _physicalFormHeart[i].enabled = false;
            }
        }
    }
}
