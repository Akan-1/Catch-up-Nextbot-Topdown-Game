using UnityEngine;
using UnityEngine.Events;

public class KeyTaskItem : MonoBehaviour, ITask
{
    public UnityEvent OnUse { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            OnUse?.Invoke();
        }
    }
}
