using UnityEngine;
using UnityEngine.UI;

public class ModesButton : MonoBehaviour
{
    [SerializeField] private Button _surviveButtons, _sandboxButtons;

    private void Start()
    {
        _surviveButtons.onClick.AddListener(() => ModesSelector.singleton.SelectSurvive());
        _sandboxButtons.onClick.AddListener(() => ModesSelector.singleton.SelectSandbox());
    }
}
