using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider BGMSlider;
    public Text BGMVolumeText;
    private int BGMVolume;

    public Slider SFXSlider;
    public Text SFXVolumeText;
    private int SFXVolume;

    public int screenWidth;
    public int screenHeight;
    public int isFull;
    public bool isFullscreen;

    public float BGMVolumeData;
    public float SFXVolumeData;

    // Start is called before the first frame update
    public void Initialize()
    {
        screenWidth = PlayerPrefs.GetInt("screenWidth", 1600);
        screenHeight = PlayerPrefs.GetInt("screenHeight", 900);
        isFull = PlayerPrefs.GetInt("isFullscreen", 1);
        if (isFull == 1)
        {
            isFullscreen = true;
        }
        else if (isFull == 0)
        {
            isFullscreen = false;
        }
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen);

        BGMVolumeData = PlayerPrefs.GetFloat("BGMVolume", 1f);
        mixer.SetFloat("BGM", Mathf.Log10(BGMVolumeData) * 20);
        BGMSlider.value = BGMVolumeData;
        BGMVolume = Mathf.FloorToInt(BGMVolumeData * 100);
        BGMVolumeText.text = BGMVolume + "%";

        SFXVolumeData = PlayerPrefs.GetFloat("SFXVolume", 1f);
        mixer.SetFloat("SFX", Mathf.Log10(SFXVolumeData) * 20);
        SFXSlider.value = SFXVolumeData;
        SFXVolume = Mathf.FloorToInt(SFXVolumeData * 100);
        SFXVolumeText.text = SFXVolume + "%";
    }

    public void SetBGMVolume(float sliderValue)
    {
        BGMVolumeData = sliderValue;
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        BGMVolume = Mathf.FloorToInt( sliderValue * 100);
        BGMVolumeText.text = BGMVolume + "%";
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float sliderValue)
    {
        SFXVolumeData = sliderValue;
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        SFXVolume = Mathf.FloorToInt(sliderValue * 100);
        SFXVolumeText.text = SFXVolume + "%";
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetResolution(Dropdown dropdownValue)
    {
        switch (dropdownValue.value)
        {
            case 0:
                screenWidth = 1920;
                screenHeight = 1080;
                isFullscreen = true;
                break;
            case 1:
                screenWidth = 1920;
                screenHeight = 1080;
                isFullscreen = false;
                break;
            case 2:
                screenWidth = 1600;
                screenHeight = 900;
                isFullscreen = true;
                break;
            case 3:
                screenWidth = 1600;
                screenHeight = 900;
                isFullscreen = false;
                break;
        }
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen);
        PlayerPrefs.SetInt("screenWidth", screenWidth);
        PlayerPrefs.SetInt("screenHeight", screenHeight);
        if (isFullscreen)
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
        PlayerPrefs.Save();
    }
}
