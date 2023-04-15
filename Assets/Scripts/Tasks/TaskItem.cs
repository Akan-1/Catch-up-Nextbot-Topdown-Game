using System;
using UnityEngine;

public class TaskItem : MonoBehaviour
{
    public event Action _onUse;
    [SerializeField] private bool _delete;
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
            gameObject.SetActive(!_delete);
            _canUse = false;
        }
    }
}
