using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using StarterAssets;

public class Settings : MonoBehaviour
{
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
    }
    public ThirdPersonController thirdPersonController;
    public Slider sensitivitySlider;

    // You might already have an Awake or Start method where you set up the initial values
    private void Start()
    {
        // Initialize the slider value to match the current mouse sensitivity
        sensitivitySlider.value = thirdPersonController.mouseSensitivity;

        // Add the slider event listener
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    // This method will be called whenever the slider value changes
    public void SetMouseSensitivity(float sensitivity)
    {
        // Apply the new sensitivity to the ThirdPersonController
        thirdPersonController.mouseSensitivity = sensitivity;
    }
}
