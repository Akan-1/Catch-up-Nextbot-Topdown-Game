using UnityEngine;

public enum Modes
{
    Survive,
    Sandbox
}

public class ModesSelector : MonoBehaviour
{
    public static ModesSelector singleton;
    private Modes _modes;

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSurvive()
    {
        _modes = Modes.Survive;
    }
    
    public void SelectSandbox()
    {
        _modes = Modes.Sandbox;
    }

    public Modes GetMod() => _modes;
}
