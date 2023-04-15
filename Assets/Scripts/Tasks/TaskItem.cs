using UnityEngine;
using UnityEngine.Events;

public class TaskItem : MonoBehaviour
{
    public UnityEvent onUse;
    [SerializeField] private bool _delete;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerController _)) return;
        onUse?.Invoke();
        gameObject.SetActive(!_delete);
    }
}
