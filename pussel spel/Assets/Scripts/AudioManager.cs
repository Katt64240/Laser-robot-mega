using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] soundtracks;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    int currentSound = 0;
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) //Select a random clip to play if no clip is playing
        {
            int nextSound = Random.Range(0, soundtracks.Length);
            audioSource.clip = soundtracks[nextSound];
            audioSource.Play(0);
        }
    }
}
