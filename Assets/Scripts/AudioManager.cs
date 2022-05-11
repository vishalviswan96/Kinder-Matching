using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip flip, win, correct, error;
    public AudioSource audio;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip()
    {
        audio.clip = flip;
        audio.Play();
    }
    public void WinFx()
    {
        audio.clip = win;
        audio.Play();
    }
    public void Correct()
    {
        audio.clip = correct;
        audio.Play();
    }
    public void Error()
    {
        audio.clip = error;
        audio.Play();
    }

}
