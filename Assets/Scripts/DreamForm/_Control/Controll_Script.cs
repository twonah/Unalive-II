using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class Controll_Script : MonoBehaviour
{
    [SerializeField] private PlayerMovement PM; //Players Movement
    [SerializeField] private DreamForm_Movement DM; //DreamForm_Movement script
    [SerializeField] private DreamForm_Punch DP; //DreamForm_Movement script
    [SerializeField] private GameObject _DreamWalk;
    [SerializeField] private Transform _parent; // Players location

    [SerializeField] private CinemachineVirtualCamera _dreamVirtualCam;

    private Animator _camera;
    private PlayersControlls _control;
    public bool isDreamWalker = false;

    void Start()
    {
        _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);// Change the opactiy to clear 
        DM.enabled = DM.enabled;

        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();

        _dreamFormCollider.enabled = false;

        //_DreamWalk.GetComponent<Rigidbody2D>().isKinematic = false;
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
        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();

        if (_control.Main.Switch.triggered) //E
        {
            PM.enabled = !PM.enabled; // switch to Dreamwalk
            DP.enabled = !DP.enabled;

            DM.enabled = !DM.enabled; // switch to Player

            if (!PM.enabled) // switch to Dreamwalk
            {
                isDreamWalker = true;

                _dreamFormCollider.enabled = true;

                //_DreamWalk.GetComponent<Rigidbody2D>().isKinematic = true;

                CameraPlay("Base Layer.DreamWalkCam");

                Vector3 oldPos = _DreamWalk.transform.position;
                DreamWalkerPosition(0.5f, 0.1f, 0f); // spawns next to the player
                Vector3 newPos = _DreamWalk.transform.position;
                _dreamVirtualCam.OnTargetObjectWarped(_DreamWalk.transform, newPos - oldPos);

                SetActive(1);

                print("YOU ARE PLAYING AS DREAMWALKER");
            }


            if (!DM.enabled) // switch to Player
            {
                isDreamWalker = false;

                _dreamFormCollider.enabled = false;

                //_DreamWalk.GetComponent<Rigidbody2D>().isKinematic = false;

                CameraPlay("Base Layer.PlayerCam");

                //DreamWalkerPosition(0.5f, 0f, 0f);

                SetActive(0);

                //_DreamWalk.transform.SetParent(_parent);

                print("YOU ARE PLAYING AS PLAYER");
            }
        }


    }



    private void CameraPlay(string CamName)
    {
        _camera.Play(CamName, 0, 0.25f);

    }

    private void DreamWalkerPosition(float x, float y, float z)
    {
        _DreamWalk.transform.position = _parent.transform.TransformPoint(new Vector3(x, y, z));
    }

    private void SetActive(int OpaNum)
    {
        _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, OpaNum);
    }

}

