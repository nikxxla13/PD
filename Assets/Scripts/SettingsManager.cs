using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider musicSlider;
    public AudioSource musicSource;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        musicSlider.value = musicSource.volume;
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}

