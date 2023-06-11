using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private ButtonsFunctional _buttons;
    [SerializeField] private GameObject _winPanel, _losePanel;

    private void Start()
    {
        _level = PlayerPrefs.GetInt("Level", 1);
        _levelText.text = $"Level {_level}";
        Time.timeScale = 1;
    }

    public void Win()
    {
        _level++;
        PlayerPrefs.SetInt("Level", _level);
        _winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        if(PlayerPrefs.GetInt("HighLevel") < _level)
            PlayerPrefs.SetInt("HighLevel", _level);
        PlayerPrefs.SetInt("Level", 1);
        _losePanel.SetActive(true);
    }
}
