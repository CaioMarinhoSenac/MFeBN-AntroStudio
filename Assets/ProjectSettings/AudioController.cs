using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource backgoundMusic;
    public AudioClip music;

    private void Start()
    {
        AudioClip CurrentMusic = music;
        backgoundMusic.clip = CurrentMusic;
        backgoundMusic.loop = true;
        backgoundMusic.Play();
    }
}
