using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : DontDestroySingle<BGMManager>
{
    [SerializeField] private AudioClip title;
    [SerializeField] private AudioClip inGame;
    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TitleBGM();
    }

    public void TitleBGM()
    {
        if (audioSource.clip == title)
            return;

        audioSource.clip = title;
        audioSource.Play();
    }

    public void InGameBGM()
    {
        if (audioSource.clip == inGame)
            return;

        audioSource.clip = inGame;
        audioSource.Play();
    }
}
