using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _inGameUI;

    private HitPoints _dreamFormHitpoints;
    private HitPoints _physicalFormHitpoints;

    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private float _gameOverDelay = 0f;

    public bool _IsGamePaused = false;
    public bool _isGameOver = false;

    private GameObject levelLoader;
    private GameObject dreamform;
    private GameObject player;

    private Controll_Script control;
    private PauseMenu pauseScript;

    //[SerializeField] UI_Cooldown uc;

    private bool isRestart;
    private float showTime;
    private bool isLoadScene;
    private string loadsceneName;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //uc = FindObjectOfType<UI_Cooldown>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
        dreamform = GameObject.FindGameObjectWithTag("DreamForm");
        player = GameObject.FindGameObjectWithTag("Player");
        control = FindObjectOfType<Controll_Script>();
        pauseScript = FindObjectOfType<PauseMenu>();

        _dreamFormHitpoints = dreamform.GetComponent<HitPoints>();
        _physicalFormHitpoints = player.GetComponent<HitPoints>();
    }
    void Update()
    {
        //Debug.Log(Time.timeScale);

        if(_physicalFormHitpoints._CurrentHitPoints <= 0)
        {
            if(!_IsGamePaused)
            {
                //StartCoroutine(GameOverDelay());
                GameOverMenu();
                //Debug.Log("TEst");
            }
        }

        if(control._IsPlayerDead)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            pauseScript.enabled = false;
        }

        if(isRestart)
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                _gameOverUI.SetActive(false);
                isRestart = false;
                _IsGamePaused = false;
            }
        }

        if (isLoadScene)
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(loadsceneName);
                _gameOverUI.SetActive(false);
                isRestart = false;
                _IsGamePaused = false;
                isLoadScene = false;
            }
        }

    }
    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(_gameOverDelay);
        _isGameOver = true;
        //GameOverMenu();
        _gameOverUI.SetActive(true);
        _IsGamePaused = true;
        _pauseMenu._IsGamePaused = false;
        _pauseMenu.enabled = false;
        _pauseUI.SetActive(false);
        _inGameUI.SetActive(false);
        Time.timeScale = 0f;

    }

    private void GameOverMenu()
    {
        if(!_isGameOver)
        {
            showTime = Time.time + _gameOverDelay;
            _isGameOver = true;
        }

        if(Time.time >= showTime && !_IsGamePaused)
        {
            _gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            _IsGamePaused = true;
            _pauseMenu._IsGamePaused = false;
            _pauseMenu.enabled = false;
            _pauseUI.SetActive(false);
            _inGameUI.SetActive(false);
            //Debug.Log("DEad");
        }
    }

    public void BackToMainMenu(string SceneName)
    {
        levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
        loadsceneName = SceneName;
        Time.timeScale = 1f;
        isLoadScene = true;
    }

    public void Restart()
    {
        levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
        Time.timeScale = 1f;
        isRestart = true;
    }

}
