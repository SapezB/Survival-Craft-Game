using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") &&  !audioSource.isPlaying) // Check if the colliding object is the player
        {
            audioSource.Play(); // Play the audio
        }
    }
}