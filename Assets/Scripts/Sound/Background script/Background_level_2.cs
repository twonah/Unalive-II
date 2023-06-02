using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_level_2 : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.StopBGM(audioManager.labbackground);
        audioManager.StopBGM(audioManager.neoncitybackground);

    }
    void Start()
    {
        audioManager.PlayBGM2(audioManager.neoncitybackground);
    }
}
