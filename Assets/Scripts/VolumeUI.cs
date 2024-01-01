using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] AudioSource m_MyAudioSource;
    Slider m_slider;
    //Value from the slider, and it converts to volume level
    float m_MySliderValue = 0.5f;

    void Start()
    {
        //Initiate the Slider value to half way
        m_slider = GetComponent<Slider>();
        m_MySliderValue = m_slider.value;
        m_MyAudioSource.volume = m_MySliderValue;
    }

    void Update()
    {
        //Makes the volume of the Audio match the Slider value
        m_MyAudioSource.volume = m_slider.value;
    }
}
