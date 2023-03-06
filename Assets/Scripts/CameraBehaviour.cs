using System.Collections;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Transform _target;

    private void Awake()
    { 
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
