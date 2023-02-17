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
    public bool isDreamWalkerToDreamform = false;
    public bool isDreamWalkerToPlayer = false;
    public bool isDreamform = false;
    public bool isDreamWalker = false;      //Might not use but later

    void Start()
    {
        _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);// Change the opactiy to clear 
        DM.enabled = DM.enabled;

        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();

        _dreamFormCollider.enabled = false;

        isDreamWalkerToDreamform = false;
        isDreamWalkerToPlayer = false;
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
                StartCoroutine(TransformToDreamform());

                _dreamFormCollider.enabled = true;

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
                StartCoroutine(TransformToPlayer());

                _dreamFormCollider.enabled = false;

                CameraPlay("Base Layer.PlayerCam");

                print("YOU ARE PLAYING AS PLAYER");
            }
        }
    }
    private IEnumerator TransformToDreamform()
    {
        isDreamWalkerToDreamform = true;
        yield return new WaitForSeconds(1f);
        isDreamform = true;
        isDreamWalkerToDreamform = false;
    }

    private IEnumerator TransformToPlayer()
    {
        isDreamWalkerToPlayer = true;
        yield return new WaitForSeconds(1f);
        isDreamform = false;
        isDreamWalkerToPlayer = false;
        SetActive(0);
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

