using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip labbackground;
    public AudioClip neoncitybackground;
    public AudioClip playerJump;
    public AudioClip playerhurt;
    public AudioClip playerdash;
    public AudioClip playerfootstep;
    public AudioClip spirithurt;
    public AudioClip spiritdash;
    public AudioClip enemydeath;
    public AudioClip enemyhurt;
    public AudioClip explosion;
    public AudioClip tankhurt;
    public AudioClip bosshurt;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //musicSource.clip = labbackground;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopBGM(AudioClip clip)
    {
        musicSource.GetComponent<AudioSource>().Stop();
    }
    //public void PlayBGM(AudioClip clip)
    //{
    //    musicSource.GetComponent<AudioSource>().Play();
    //    musicSource.Play();
    //}
    public void PlayBGM1(AudioClip clip)
    {
        musicSource.clip = labbackground;
        musicSource.Play();
    }
    public void PlayBGM2(AudioClip clip)
    {
        musicSource.clip = neoncitybackground;
        musicSource.Play();
    }
}
