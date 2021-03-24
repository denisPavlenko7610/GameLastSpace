using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public AudioMixerGroup _mixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;

    private void Start()
    {
        _musicSlider.onValueChanged.AddListener (delegate {ChangeMusicVolume ();});
        _effectsSlider.onValueChanged.AddListener (delegate {ChangeEffectsVolume ();});
    }


    public void ChangeMusicVolume()
    {
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, _musicSlider.value));
    }
    
    public void ChangeEffectsVolume()
    {
        _mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, _effectsSlider.value));
    }
}
