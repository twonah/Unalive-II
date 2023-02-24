using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;

    [SerializeField] private HitPoints _dreamFormHitpoints;
    [SerializeField] private HitPoints _physicalFormHitpoints;

    [SerializeField] private float _gameOverDelay = 0f;

    private bool _IsGamePaused = false;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (_dreamFormHitpoints._CurrentHitPoints <= 0 && _physicalFormHitpoints._CurrentHitPoints <= 0)
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
    }

    public void BackToMainMenu()
    {
        _gameOverUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }

    public void Restart()
    {
        _gameOverUI.SetActive(false);
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }

}
