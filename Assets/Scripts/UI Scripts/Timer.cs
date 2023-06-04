using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        _timerText.text = time.ToString("F2");
    }

    private void Update()
    {
        time += Time.deltaTime;
        _timerText.text = time.ToString("F2");
    }

    public void TimerStop(bool stop)
    {
        if (stop)
        {
            _timerText.enabled = false;
            enabled = false;
        }
    }
}
