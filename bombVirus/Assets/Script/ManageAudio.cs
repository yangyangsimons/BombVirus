using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAudio : MonoBehaviour
{
    public static ManageAudio Instance;
    public AudioClip setBoom, bombing,loading;

    private AudioSource source;

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();

    }

    public void SetBoombg()
    {
        source.clip = setBoom;
        source.Play();

    }
    public void Bombingbg()
    {
        source.clip = bombing;
        source.Play();

    }
    public void Loading()
    {
        source.clip = loading;
        source.Play();

    }
}
