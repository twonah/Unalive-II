using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_level_0 : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.StopBGM(audioManager.labbackground);
        audioManager.StopBGM(audioManager.neoncitybackground);
    }
}
