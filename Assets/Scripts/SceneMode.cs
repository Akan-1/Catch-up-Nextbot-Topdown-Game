using UnityEngine;
using UnityEngine.Events;

public class SceneMode : MonoBehaviour
{
    [SerializeField] private UnityEvent _onSurvive;
    [SerializeField] private UnityEvent _onSandbox;
    [SerializeField] private Modes _defaultMode = Modes.Sandbox;

    private void Start()
    {
        Modes mode = _defaultMode;
        if(ModesSelector.singleton != null)
           mode = ModesSelector.singleton.GetMod();

        switch (mode)
        {
            case Modes.Survive:
                _onSurvive?.Invoke();
                break;
            case Modes.Sandbox:
                _onSandbox?.Invoke();
                break;
        }
    }
}
