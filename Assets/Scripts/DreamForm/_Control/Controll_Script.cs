using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class Controll_Script : MonoBehaviour
{
    [SerializeField] private PlayerMovement PM; //Players Movement
    [SerializeField] private DreamForm_Movement DM; //DreamForm_Movement script
    [SerializeField] private DreamForm_Punch DP; //DreamForm_Movement script
    [SerializeField] private GameObject _DreamWalk;
    [SerializeField] private Rigidbody2D _DW_rb; // DreamFors_Rigid Body
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _parent; // Players location
    [SerializeField] private CinemachineVirtualCamera _dreamVirtualCam;
    [SerializeField] public UI_Cooldown _Cooldown; // Cooldown script

    public int CoolCheck; // checks to activate cooldown 

    private Animator _camera;
    private PlayersControlls _control;
    public bool isDreamWalkerToDreamform = false;
    public bool isDreamWalkerToPlayer = false;
    public bool isDreamform = false;
    public bool isPlayer = false;

    public bool _IsDreamformDead;
    public bool _IsPlayerDead;

    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    void Start()
    {
        _DreamWalk.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);// Change the opactiy to clear 
        DM.enabled = DM.enabled;

        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();


        _dreamFormCollider.enabled = false;

        isDreamWalkerToDreamform = false;
        isDreamWalkerToPlayer = false;

        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
    }

    private void Update()
    {   
        if(_DreamWalk.GetComponent<HitPoints>()._CurrentHitPoints <= 0) //Force to turn to the player
        {
            _IsDreamformDead = true;
            StartCoroutine(ForceToTransformToPlayer());
        }
        else
        {
            _IsDreamformDead = false;
        }

        if(isDreamform && _Cooldown._CurrentEnergy <= 0)
        {
            StartCoroutine(ForceToTransformToDreamform());
        }

        if (_player.GetComponent<HitPoints>()._CurrentHitPoints <=0 ) //Force to turn to Dreamform
        {
            _IsPlayerDead = true;
            StartCoroutine(ForceToTransformToDreamform());  
        }
        else
        {
            _IsPlayerDead = false;
        }

        if (_IsPlayerDead == false && DM.enabled == !DM.enabled) // dreamform follows player when deactivated
        {
            DreamWalkerPosition(0.5f, 0.1f, 0f); // follows the player while deactivated
        }

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
            if (_Cooldown._CurrentEnergy > 0) // checks if cooldown is bigger 0 before switching
            {
                PM.enabled = !PM.enabled; // switch to Dreamwalk
                //Debug.Log("Dreamwalk");
                DM.enabled = !DM.enabled; // switch to Player
                DP.enabled = !DP.enabled;
            }
            else if (_Cooldown._CurrentEnergy <= 0)
            {
                DM.enabled = !DM.enabled; // switch to Player
                PM.enabled = enabled;
            }

            if (!PM.enabled) // switch to Dreamwalk
            {
                PM.StopWalking();

                StartCoroutine(TransformToDreamform());


                _dreamFormCollider.enabled = true;

                CameraPlay("Base Layer.DreamWalkCam");

                Vector3 oldPos = _DreamWalk.transform.position;
                DreamWalkerPosition(0.5f, 0.1f, 0f); // spawns next to the player
                Vector3 newPos = _DreamWalk.transform.position;
                _dreamVirtualCam.OnTargetObjectWarped(_DreamWalk.transform, newPos - oldPos);

                SetActive(1);
                print("YOU ARE PLAYING AS DREAMWALKER");

                CoolCheck = 1;
                _Cooldown.IfDreamForm(CoolCheck); // drains energy
            }

            if (!DM.enabled) // switch to Player
            {
                DP.enabled = false;

                _DW_rb.velocity = new Vector2(0, 0);

                StartCoroutine(TransformToPlayer());

                _dreamFormCollider.enabled = false;

                CameraPlay("Base Layer.PlayerCam");

                print("YOU ARE PLAYING AS PLAYER");

                CoolCheck = 2;
                _Cooldown.IfDreamForm(CoolCheck); // stops drain
            }
        }
    }

    private IEnumerator TransformToDreamform()
    {
        isDreamWalkerToDreamform = true;
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0.587f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 20f, m_Vignette);
        Debug.Log("Post effect : On");
        yield return new WaitForSeconds(0.5f);
        isDreamform = true;
        isDreamWalkerToDreamform = false;
    }

    private IEnumerator TransformToPlayer()
    {
        isDreamWalkerToPlayer = true;
        m_Vignette.enabled.Override(false);
        m_Vignette.intensity.Override(0f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, m_Vignette);
        Debug.Log("Post effect : Off");
        yield return new WaitForSeconds(0.5f);
        isDreamform = false;
        isDreamWalkerToPlayer = false;
        SetActive(0);
    }

    public IEnumerator ForceToTransformToPlayer()   //Not done      //This one is trans to player
    {
        //StartCoroutine(TransformToPlayer());

        PM.enabled = true;
        //PM.enabled = !PM.enabled; // switch to Dreamwalk
        DP.enabled = !DP.enabled;

        DM.enabled = !DM.enabled; // switch to Player

        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();

        isDreamWalkerToPlayer = true;
        yield return new WaitForSeconds(1f);
        isDreamform = false;
        isDreamWalkerToPlayer = false;
        SetActive(0);

        _dreamFormCollider.enabled = false;

        CameraPlay("Base Layer.PlayerCam");
    }

    public IEnumerator ForceToTransformToDreamform() // force switch to Dreamform when player hp is zero //This one is trans to dreamform
    {
        PM.enabled = !PM.enabled; // switch to dreamwalk

        DP.enabled = true;
        DM.enabled = true;

        Collider2D _dreamFormCollider = _DreamWalk.GetComponent<Collider2D>();
        isDreamWalkerToDreamform = true;

        yield return new WaitForSeconds(1f);
        isDreamform = true;
        isDreamWalkerToDreamform = false;
        CameraPlay("Base Layer.DreamWalkCam");
        //StartCoroutine(TransformToDreamform());
        SetActive(1);
        //print("YOU ARE PLAYING AS DREAMWALKER");
        _dreamFormCollider.enabled = true;
        CoolCheck = 1;
        _Cooldown.IfDreamForm(CoolCheck); // drains energy
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

