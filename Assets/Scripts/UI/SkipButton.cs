using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private GameObject levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        if(_sceneName != "")
        {
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(_sceneName);
            }
        }
    }

    public void Skip(string sceneName)
    {
        _sceneName = sceneName;
        levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
    }
}
