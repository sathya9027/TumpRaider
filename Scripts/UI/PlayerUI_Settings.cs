using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerUI_Settings : MonoBehaviour
{
    [SerializeField] Sprite sfxAudioOn;
    [SerializeField] Sprite sfxAudioOff;
    [SerializeField] Image sfxButtonImage;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject settingsScreen;

    private void Start()
    {
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
        sfxSlider.maxValue = 0;
        sfxSlider.minValue = -80;
        sfxSlider.value = PlayerPrefs.GetFloat("SFXAudio");
    }

    private void Update()
    {
        SetSFXAudio();
    }

    public void ReturnMainMenu()
    {
        settingsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    private void SetSFXAudio()
    {
        audioMixer.SetFloat("MasterVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXAudio", sfxSlider.value);
        if (sfxSlider.value == 0)
        {
            sfxButtonImage.sprite = sfxAudioOn;
        }
        else if (sfxSlider.value == -80)
        {
            sfxButtonImage.sprite = sfxAudioOff;
        }
    }
}
