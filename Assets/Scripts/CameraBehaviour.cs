using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;

	public Vector2 maxValues;
	public Vector2 minValues;

	public Transform Target
	{
		get => _target;
		set => _target = value;
	}

	private void FixedUpdate()
	{
		Vector3 desiredPosition = Target.position + _offset;

		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
