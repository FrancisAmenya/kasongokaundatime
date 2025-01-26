using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerControllerAuto : MonoBehaviour
{
    public AudioMixer masterMixer;

    float sliderMusicValue, sliderSfxValue;

    void Start()
    {

        sliderMusicValue = PlayerPrefs.GetFloat("amgMusicLevel", 0.1f);

        sliderSfxValue = PlayerPrefs.GetFloat("amgEffectsLevel", 0.28f);

        masterMixer.SetFloat("amgMusicLevel", Mathf.Log10(sliderMusicValue) * 20);

        masterMixer.SetFloat("amgEffectsLevel", Mathf.Log10(sliderSfxValue) * 20);

    }


}
