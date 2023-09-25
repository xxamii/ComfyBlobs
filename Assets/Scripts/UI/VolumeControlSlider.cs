using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControlSlider : MonoBehaviour
{

    public AudioMixer mixer;
    public string volumeParameter;
    public Slider slider;
    public TMP_Text _text;


    private void Awake()
    {
        slider.onValueChanged.AddListener(SliderValueChanged);
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    public void SliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * 20);
        _text.text = Mathf.FloorToInt(value * 100).ToString();
    }

   
}
