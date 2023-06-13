using System;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _parameterName;
    [SerializeField] private string _sliderName;

    private void Start()
    {
        if (_audioMixer != null)
        {
            float volume = PlayerPrefs.GetFloat("AudioVolume");
            _audioMixer.SetFloat(_parameterName, Mathf.Log10(volume) * 20);
            _volumeSlider.value = volume;
            _text.text = _sliderName + Mathf.Round(volume * 100) + "%";
        }
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat(_parameterName, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("AudioVolume", volume);
        _text.text = _sliderName + Mathf.Round(volume * 100) + "%";
    }
}