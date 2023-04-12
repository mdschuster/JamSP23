using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    //singleton part
    private static MusicManager _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public static MusicManager instance()
    {
        return _instance;
    }

    //*******************************************

    private AudioSource audioSource;
    public AudioClip musicClip;

    // Start is called before the first frame update
    void Start()
    {
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

}
