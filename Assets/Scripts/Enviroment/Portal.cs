using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string _sceneName;
     private GameObject levelLoader;
     private GameObject control;
     private bool isWarp;
     private GameOver gameOverScript;
     private PauseMenu pauseMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
        control = GameObject.FindGameObjectWithTag("SwitchControl");
        gameOverScript = FindObjectOfType<GameOver>();
        pauseMenuScript = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWarp && _sceneName != "")
        {
            if(levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(_sceneName);
                isWarp = false;
                gameOverScript.enabled = true;
                pauseMenuScript.enabled = true;
                control.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            control.SetActive(false);
            levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");
            isWarp = true;
            gameOverScript.enabled = false;
            pauseMenuScript.enabled = false;
        }
    }
}
