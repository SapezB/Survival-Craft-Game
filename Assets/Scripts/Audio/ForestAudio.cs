using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ForestAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}