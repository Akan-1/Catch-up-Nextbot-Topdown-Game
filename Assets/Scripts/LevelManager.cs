using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private ButtonsFunctional _buttons;

    private void Start()
    {
        _level = PlayerPrefs.GetInt("Level", 1);
        _levelText.text = $"Level {_level}";
    }

    public void MoveToNextLevel()
    {
        _level++;
        PlayerPrefs.SetInt("Level", _level);
        _buttons.AsyncLoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
    }

    public void Lose()
    {
        PlayerPrefs.SetInt("HighLevel", _level);
        PlayerPrefs.SetInt("Level", 1);

        _buttons.LoadScene(0);
    }
}
