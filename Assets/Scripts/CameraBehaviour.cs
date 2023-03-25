using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;

	public Vector2 maxValues;
	public Vector2 minValues;

	Bounds per;

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

	//private void OnDrawGizmosSelected()
	//{
	//	// Draw vertical borders
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawLine(new Vector3(minX.x, minY.x, 0f), new Vector3(minX.x, minY.y, 0f));
	//	Gizmos.DrawLine(new Vector3(minX.y, minY.x, 0f), new Vector3(minX.y, minY.y, 0f));

	//	// Draw horizontal borders
	//	Gizmos.color = Color.blue;
	//	Gizmos.DrawLine(new Vector3(minX.x, minY.x, 0f), new Vector3(minX.y, minY.x, 0f));
	//	Gizmos.DrawLine(new Vector3(minX.x, minY.y, 0f), new Vector3(minX.y, minY.y, 0f));
	//}

}
