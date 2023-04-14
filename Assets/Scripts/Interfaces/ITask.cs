using UnityEngine.Events;

public interface ITask
{
    public UnityEvent OnUse { get; set; }
}
