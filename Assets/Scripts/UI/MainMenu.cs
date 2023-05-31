using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject levelLoader;

    public string loadsceneName;
    public bool loadTransition;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        if(loadTransition)
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(loadsceneName);
            }
        }
        else
        {
            if(loadsceneName != "")
            {
                SceneManager.LoadScene(loadsceneName);
            }

        }

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        loadsceneName = sceneName;
    }

    public void LoadTransition(bool haveTransion)
    {
        if(haveTransion)
        {
            levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
            loadTransition = true;
        }
    }
}
