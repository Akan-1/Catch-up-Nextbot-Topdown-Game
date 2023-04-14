using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private float _count;

    public UnityEvent OnFinish;

    public void AddCount()
    {
        _count--;
        if (_count == 0)
        {
            OnFinish?.Invoke();
        }
    }
}
