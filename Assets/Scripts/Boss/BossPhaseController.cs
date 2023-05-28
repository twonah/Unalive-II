using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField] private float _phaseStartDelay = 0f;

    [SerializeField] private BossControlP1 _bossPhase1;
    [SerializeField] private BossAnimation _bossPhase1Anim;
    [SerializeField] private BossControlP2 _bossPhase2;
    [SerializeField] private BossP2Animation _bossPhase2Anim;

    [SerializeField] private GameObject _bossP1;
    [SerializeField] private GameObject _bossP1Prop;
    [SerializeField] private GameObject _bossP2;
    [SerializeField] private GameObject _bossP2Prop;

    [SerializeField] private Transform _starterPoint2;
    [SerializeField] private Transform _starterPoint3;

    [SerializeField] private Animator _animBossP1;
    [SerializeField] private Animator _animBossP2;

    private GameObject _switchControl;
    private GameObject _player;
    private GameObject _dreamform;

    private Controll_Script _controlScript;

    public float _warpTime;
    public bool _startNewPhase;
    public bool _warpPlayer;
    public bool _P1Done;
    public bool _P2Done;
    public bool _P3Done;

    private bool isBack;

    // Start is called before the first frame update
    void Start()
    {
        _switchControl = GameObject.FindWithTag("SwitchControl");
        _player = GameObject.FindWithTag("Player");
        _dreamform = GameObject.FindWithTag("DreamForm");

        _controlScript = _switchControl.GetComponent<Controll_Script>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        PhaseControl();
    }

    private void PhaseControl()
    {
        //Phase 1
        if (_bossPhase1.EnterP2 && _bossP1.activeSelf)    //Start phase 2
        {
            //Debug.Log("Enter Phase 2");
            if (!_startNewPhase && _bossP1.activeSelf)
            {
                _warpTime = Time.time + _phaseStartDelay;
                _startNewPhase = true;
            }

            if (Time.time >= _warpTime)
            {
                _bossPhase1Anim.enabled = false;
                _bossP1Prop.SetActive(false);

                _animBossP1.SetBool("IsFall", false);
                _animBossP1.SetBool("IsWarp", true);
            }

            _bossP2.SetActive(true);
        }
        else if(!_bossPhase1.EnterP2) //phase 1 not finish
        {
            _bossP2.SetActive(false);
        }

        if (!_bossP1.activeSelf)    // Check from animation warp of Boss
        {
            if(!_warpPlayer && !_P1Done)
            {
                _player.transform.position = _starterPoint2.position;
                _dreamform.transform.position = _starterPoint2.position;
                _warpPlayer = true;
                _startNewPhase = false;
                _P1Done = true;
            }
            else if(!_startNewPhase && _P1Done)
            {
                _warpPlayer = false;
            }
        }

        //Phase 2
        if(_P1Done && !_P2Done) //Force to Dreamform
        {
            _controlScript.isDreamWalkerToDreamform = true;
            StartCoroutine(_controlScript.ForceToTransformToDreamform());
            _player.GetComponent<UI_Cooldown>()._CurrentEnergy = _player.GetComponent<UI_Cooldown>()._MaxEnergy;
        }


        if (_P1Done && _P2Done)
        {
            if (!isBack)
            {
                StartCoroutine(_controlScript.ForceToTransformToPlayer());
                isBack = true;
                //Debug.Log("Back to Player");
            }
        }

        if(_bossPhase2.EnterP3 && _bossP2.activeSelf)
        {
            //Debug.Log(Time.time + " | " + _warpTime);
            if (!_startNewPhase && _bossP2.activeSelf)
            {
                _warpTime = Time.time + _phaseStartDelay;
                _startNewPhase = true;
                //Debug.Log("Time start");
            }

            if (Time.time >= _warpTime)
            {
                _bossPhase2Anim.enabled = false;
                _bossP2Prop.SetActive(false);

                _animBossP2.SetBool("IsFall", false);
                _animBossP2.SetBool("IsCall", false);
                _animBossP2.SetBool("IsWarp", true);
            }
        }

        if (!_bossP2.activeSelf && !_bossP1.activeSelf)    // Check from animation warp of Boss
        {
            if (!_warpPlayer && !_P2Done)
            {
                //Debug.Log("Warp to 3");
                _player.transform.position = _starterPoint3.position;
                _dreamform.transform.position = _starterPoint3.position;
                _warpPlayer = true;
                _startNewPhase = false;
                _P2Done = true;
            }
            else if (!_startNewPhase && _P2Done)
            {
                _warpPlayer = false;
            }
        }

    }
}
