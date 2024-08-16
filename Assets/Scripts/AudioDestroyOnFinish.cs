using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyOnFinish : MonoBehaviour
{
    private AudioSource sfxAudio;
    void Start()
    {
        sfxAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!sfxAudio.isPlaying) Destroy(this.gameObject);
    }
}
