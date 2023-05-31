using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] public bool _IsGamePaused = false;

    [SerializeField] private GameObject _pauseMenuUI;

    private GameObject levelLoader;

    private bool loadTransition;
    private bool isRestart;
    private bool isLoadScene;
    private string loadsceneName;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {   
        if(!isRestart && !isLoadScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }

        if (loadTransition && isRestart)
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                loadTransition = false;
                isRestart = false;
            }
        }

        if(loadTransition && isLoadScene)
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(loadsceneName);
                loadTransition = false;
                isLoadScene = false;
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

    public void BackToMainMenu(string SceneName)
    {
        loadTransition = true;
        loadsceneName = SceneName;
        levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
        Time.timeScale = 1f;
        _IsGamePaused = false;
        isLoadScene = true;
        _pauseMenuUI.SetActive(false);
    }
    public void Restart()
    {
        isRestart = true;
        loadTransition = true;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _IsGamePaused = false;
        levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
    }
}
