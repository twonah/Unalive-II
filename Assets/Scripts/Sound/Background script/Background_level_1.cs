using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_level_1 : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.labbackground);
    }
}
