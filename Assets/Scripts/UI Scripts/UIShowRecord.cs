using TMPro;
using UnityEngine;

public class UIShowRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text _surviveText, _sandboxText;

    private void Start()
    {
        int survive = PlayerPrefs.GetInt("HighLevel", 1);
        _surviveText.text = $"Твой рекорд: {survive}";
    }
}
