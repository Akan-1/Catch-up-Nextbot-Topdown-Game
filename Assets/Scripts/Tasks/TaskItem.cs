using UnityEngine;
using UnityEngine.Events;

public class TaskItem : MonoBehaviour
{
    public UnityEvent OnUse;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            OnUse?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
