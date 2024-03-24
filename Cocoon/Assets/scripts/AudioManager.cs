using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public AudioClip backingTrack1;
    public AudioClip backingTrack2;
    public AudioClip backingTrack3;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.clip = backingTrack1;
            audioSource.Play();

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            audioSource.clip = backingTrack2;
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            audioSource.clip = backingTrack3;
            audioSource.Play();
        }

    }
}
