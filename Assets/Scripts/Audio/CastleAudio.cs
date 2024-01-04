using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isOn = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && !audioSource.isPlaying && !isOn) // Check if the colliding object is the player
        {
            audioSource.Play(); // Play the audio
            isOn = true;
        }
        else if (other.tag == ("Player") && audioSource.isPlaying && isOn == true)
        {
            audioSource.Stop();
            isOn = false;
        }
    }
}