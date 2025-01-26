using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class AudioMixerController : MonoBehaviour
{

    public AudioMixer masterMixer;

    public UnityEngine.UI.Slider sliderMusic, sliderSfx;

    float sliderMusicValue, sliderSfxValue;


    void Start()
    {
        GetEffectsLevel();
    }


    public void GetEffectsLevel()
    {

        sliderMusic.value = PlayerPrefs.GetFloat("amgMusicLevelPref", 0.1f);

        sliderSfx.value = PlayerPrefs.GetFloat("amgEffectsLevelPref", 0.28f);

    }

    public void RestoreEffectsLevel() 
    {

        GetEffectsLevel();
        
        SetMusicLevel();
        
        SetEffectsLevel();

    }

    public void SetMusicLevel()
    {

        sliderMusicValue = sliderMusic.value;

        masterMixer.SetFloat("amgMusicLevel", Mathf.Log10(sliderMusicValue) * 20);

    }


    public void SetEffectsLevel()
    {

        sliderSfxValue = sliderSfx.value;

        masterMixer.SetFloat("amgEffectsLevel", Mathf.Log10(sliderSfxValue) * 20);

    }


    public void SaveLevels() 
    {
    
        PlayerPrefs.SetFloat("amgMusicLevelPref", sliderMusicValue);

        PlayerPrefs.SetFloat("amgEffectsLevelPref", sliderSfxValue);

    }

}