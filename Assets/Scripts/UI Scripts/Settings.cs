using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _parameterName;
    [SerializeField] private string _sliderName;

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat(_parameterName, Mathf.Log10(volume) * 20);
        _text.text = _sliderName + Mathf.Round(volume * 100) + "%";
    }
}