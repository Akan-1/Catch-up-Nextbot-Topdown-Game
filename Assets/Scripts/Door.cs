using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _onOpen;
    public bool canOpen;

    public void OpenDoor() => canOpen = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && canOpen)
        {
            _onOpen?.Invoke();
        }
    }
}
