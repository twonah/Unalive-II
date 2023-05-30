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

    [SerializeField] private HitPoints _dreamFormHitpoints;
    [SerializeField] private HitPoints _physicalFormHitpoints;

    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private float _gameOverDelay = 0f;

    private bool _IsGamePaused = false;
    private bool _isGameOver = false;

    [SerializeField] UI_Cooldown uc;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        uc = FindObjectOfType<UI_Cooldown>();
    }
    void Update()
    {
        if (_dreamFormHitpoints._CurrentHitPoints <= 0 && _physicalFormHitpoints._CurrentHitPoints <= 0)
        {
            StartCoroutine(GameOverDelay());
        }

        if(_physicalFormHitpoints._CurrentHitPoints <= 0 && uc._CurrentEnergy <= 0)
        {
            StartCoroutine(GameOverDelay());
        }
    }
    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(_gameOverDelay);
        _isGameOver = true;
        GameOverMenu();
    }

    public void GameOverMenu()
    {
        _gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        _IsGamePaused = true;
        _pauseMenu._IsGamePaused = false;
        _pauseMenu.enabled = false;
        _pauseUI.SetActive(false);
        _inGameUI.SetActive(false);

    }

    public void BackToMainMenu(string SceneName)
    {
        _gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }

    public void Restart()
    {
        _gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }

}
