using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    [SerializeField] bool detectPlayer;
    [SerializeField] Controll_Script cs;
    [SerializeField] GameObject text;
    AudioManager audioManager;
    [SerializeField] AudioSource zaWARUDO;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (text != null)
        {
            text.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cs.isDreamWalkerToDreamform && detectPlayer)
        {
            Time.timeScale = 1f;
            audioManager.resumeBGM1(audioManager.labbackground);
            detectPlayer = false;
            if (text != null)
            {
                text.SetActive(false);
            }
            gameObject.SetActive(false);
        }

        if(detectPlayer)
        {
            audioManager.StopFootstep(audioManager.playerfootstep);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            detectPlayer = true;
            if (text != null)
            {
                StartCoroutine(textDelay());
            }
            audioManager.StopFootstep(audioManager.playerfootstep);
            zaWARUDO.Play();
            StartCoroutine(audioSync());
            audioManager.pauseBGM1(audioManager.labbackground);
            StartCoroutine(hideObject());
        }
    }

    IEnumerator hideObject()
    {
        yield return new WaitForSecondsRealtime(10f);
        Time.timeScale = 1f;
        audioManager.resumeBGM1(audioManager.labbackground);
        detectPlayer = false;
        if (text != null)
        {
            text.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    IEnumerator textDelay()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        text.SetActive(true);
    }

    IEnumerator audioSync()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0.01f;
    }

    private void OnDisable()
    {
        if (text != null)
        {
            text.SetActive(false);
        }
        detectPlayer = false;
    }
}
