using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskItem : MonoBehaviour
{
    public event Action _onUse;
    [SerializeField] private bool _delete;
    [SerializeField] private UnityEvent _changeItem;
    private bool _canUse = true;

    private void Start()
    {
        _onUse += FindObjectOfType<TaskManager>().Use;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController _) && _canUse)
        {
            _onUse?.Invoke();
            _changeItem?.Invoke();
            gameObject.SetActive(!_delete);
            _canUse = false;
        }
    }
}
