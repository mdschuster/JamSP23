using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioObject : MonoBehaviour
{
    [Header("Random Clip To Play")]
    public AudioClip[] clips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (clips.Length == 0) return;
        int index = Random.Range(0, clips.Length);
        AudioClip clipToPlay = clips[index];
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }


}
