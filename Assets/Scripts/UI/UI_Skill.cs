using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Skill : MonoBehaviour
{
    [Header("Set up")]
    [SerializeField] private Image _dreamwalkBarImage;
    [SerializeField] private Image _playerCooldownImage;
    [SerializeField] private Image _dreamformCooldownImage;

    //[SerializeField] private TMP_Text _dreamwalkSkillCooldownText;
    [SerializeField] private TMP_Text _playerDashCooldownText;
    [SerializeField] private TMP_Text _dreamformDashCooldownText;

    [SerializeField] private GameObject _playerDashIcon;
    [SerializeField] private GameObject _dreamformDashIcon;

    [SerializeField] private PlayerMovement PM;
    [SerializeField] private DreamForm_Movement DM;
    [SerializeField] private UI_Cooldown DC;
    [SerializeField] private Controll_Script CS;

    [SerializeField] private Animator _anim;

    private float _playerDashCooldown;
    private float _dreamformDashCooldown;

    public float _playerDashCooldownTimer;
    public float _dreamformDashCooldownTimer;



    // Start is called before the first frame update
    void Start()
    {
        _playerDashCooldownText.gameObject.SetActive(false);
        _dreamformDashCooldownText.gameObject.SetActive(false);

        _playerCooldownImage.fillAmount = 0.0f;
        _dreamformCooldownImage.fillAmount = 0.0f;
        _dreamwalkBarImage.fillAmount = 0.0f;

        _playerDashCooldown = PM.DashingCooldown;
        _dreamformDashCooldown = DM.DashingCooldown;

        _playerDashCooldownTimer = _playerDashCooldown;
        _dreamformDashCooldownTimer = _dreamformDashCooldown;

        _dreamformDashIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.IsCooldown)
        {
            _playerDashCooldownText.gameObject.SetActive(true);

            PlayerDashIcon();
        }

        if(CS.isDreamWalkerToDreamform)
        {
            _playerDashIcon.SetActive(false);
            _dreamformDashIcon.SetActive(true);
        }
        if(CS.isDreamWalkerToPlayer)
        {
            _playerDashIcon.SetActive(true);
            _dreamformDashIcon.SetActive(false);
        }

        if (DM.IsCooldown)
        {
            _dreamformDashCooldownText.gameObject.SetActive(true);

            DreamformDashIcon();
        }

        DreamwalkSkillIcon();

    }

    private void DreamwalkSkillIcon()
    {
        // Update the energy bar
        _dreamwalkBarImage.fillAmount = DC._CurrentEnergy / DC._MaxEnergy;

        //Animation
        if(CS.isDreamWalkerToDreamform)
        {
            _anim.SetBool("DToP", false);
        }
        else if(CS.isDreamWalkerToPlayer)
        {
            _anim.SetBool("DToP", true);
        }


    }
    private void PlayerDashIcon()
    {
        _playerDashCooldownTimer -= Time.deltaTime;

        if (_playerDashCooldownTimer < 0.0f)
        {
            PM.IsCooldown = false;
            _playerDashCooldownText.gameObject.SetActive(false);
            _playerCooldownImage.fillAmount = 0.0f;
            _playerDashCooldownTimer = _playerDashCooldown;
        }
        else
        {
            _playerDashCooldownText.text = Mathf.RoundToInt(_playerDashCooldownTimer).ToString();
            _playerCooldownImage.fillAmount = _playerDashCooldownTimer / _playerDashCooldown;
        }
    }

    private void DreamformDashIcon()
    {
        _dreamformDashCooldownTimer -= Time.deltaTime;

        if (_dreamformDashCooldownTimer < 0.0f)
        {
            DM.IsCooldown = false;
            _dreamformDashCooldownText.gameObject.SetActive(false);
            _dreamformCooldownImage.fillAmount = 0.0f;
            _dreamformDashCooldownTimer = _playerDashCooldown;
        }
        else
        {
            _dreamformDashCooldownText.text = Mathf.RoundToInt(_dreamformDashCooldownTimer).ToString();
            _dreamformCooldownImage.fillAmount = _dreamformDashCooldownTimer / _dreamformDashCooldown;
        }
    }
}
