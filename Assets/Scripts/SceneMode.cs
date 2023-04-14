using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneMode : MonoBehaviour
{
    [SerializeField] private UnityEvent _onSurvive;
    [SerializeField] private UnityEvent _onSandbox;

    private void Start()
    {
        Modes mode = ModesSelector.singleton.GetMod();

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
