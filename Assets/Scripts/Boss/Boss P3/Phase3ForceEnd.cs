using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase3ForceEnd : MonoBehaviour
{
    private GameObject levelLoader;
    [SerializeField] private string SceneName;
    [SerializeField] private CoreHP coreHP;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        if(coreHP.currentHP <= 0)
        {
            levelLoader.GetComponent<Animator>().SetTrigger("LoadTransition");

            Debug.Log("TEst");
            if (levelLoader.GetComponent<SceneTransition>()._TransitionEnd)
            {
                SceneManager.LoadScene(SceneName);
            }
        }


    }
}
