using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] public bool _IsGamePaused = false;

    [SerializeField] private GameObject _pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _IsGamePaused = true;
    }

    public void ExitToMainMenu(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
        _IsGamePaused = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
