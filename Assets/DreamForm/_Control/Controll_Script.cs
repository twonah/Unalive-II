using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mygame
{ 
public class Controll_Script : MonoBehaviour
{
    [SerializeField] private PlayerMovement PM; //Players Movement
    [SerializeField] private DreamForm_Movement DM; //DreamForm_Movement script
    [SerializeField] private GameObject _DreamWalk;
    [SerializeField] private Transform _parent; // Players location

    private Animator _camera;
    private PlayersControlls _control;
    public bool isDreamWalker = false;

    void Start()
    {
            _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);// Change the opactiy to clear 
        }


    private void Awake()
    {
        _control = new PlayersControlls();
        _camera = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _control.Main.Enable();
    }

    private void OnDisable()
    {
        _control.Main.Disable();
    }

    private void FixedUpdate()
    {
        _control.Main.Switch.performed += Context => Switch();
    }


    void Switch()
    { 
        
        if (_control.Main.Switch.triggered) //E
        {
            PM.enabled = !PM.enabled; // switch to Dreamwalk
            DM.enabled = !DM.enabled; // switch to Player

            if (!PM.enabled) // switch to Dreamwalk
            {
                isDreamWalker = true;

                CameraPlay("Base Layer.DreamWalk_Cam");

                DreamWalkerPosition(1.0f, 0f, 0f); // spawns next to the player

                SetActive(1);

                print("YOU ARE PLAYING AS DREAMWALKER");
            }


            if (!DM.enabled) // switch to Player
            {
            isDreamWalker = false;

            CameraPlay("Base Layer.Player_Cam");

            DreamWalkerPosition(0.5f, 0f, 0f);

            SetActive(0);

            _DreamWalk.transform.SetParent(_parent);

            print("YOU ARE PLAYING AS PLAYER");
            }
        }
       

    }

    

    private void CameraPlay (string CamName)
    {
        _camera.Play(CamName, 0, 0.25f);

    }

    private void DreamWalkerPosition(float x, float y, float z)
    {
        _DreamWalk.transform.localPosition = new Vector3(x, y, z);
    }

    private void SetActive(int OpaNum)
    {
            _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, OpaNum);
        }

}
}
