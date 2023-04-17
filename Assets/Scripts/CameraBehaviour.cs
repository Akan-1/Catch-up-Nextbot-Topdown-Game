using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;

	private Bounds _cameraBounds;
	private Vector3 _targetPosition;

	private void Start()
	{
		var height = Camera.main.orthographicSize;
		var width = height * Camera.main.aspect;

		var screenBounds = new Bounds(Vector3.zero, new Vector3(Screen.width, Screen.height, 0));
		screenBounds.center = Camera.main.ScreenToWorldPoint(screenBounds.center);
		screenBounds.size /= Camera.main.orthographicSize * 2.0f;

		// Calculate camera bounds based on screen bounds and world bounds
		var minX = Mathf.Min(Globals.WorldBounds.min.x + width, screenBounds.min.x);
		var maxX = Mathf.Max(Globals.WorldBounds.max.x - width, screenBounds.max.x);

		var minY = Mathf.Min(Globals.WorldBounds.min.y + height, screenBounds.min.y);
		var maxY = Mathf.Max(Globals.WorldBounds.max.y - height, screenBounds.max.y);

		_cameraBounds = new Bounds();
		_cameraBounds.SetMinMax(
			new Vector3(minX, minY, 0.0f),
			new Vector3(maxX, maxY, 0.0f)
			);
	}

	public Transform Target
	{
		get => _target;
		set => _target = value;
	}

	private void FixedUpdate()
	{
		Vector3 desiredPosition = Target.position + _offset;

		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

		_targetPosition = smoothedPosition;
		_targetPosition = GetCameraBounds();
		transform.position = _targetPosition;
	}

	private Vector3 GetCameraBounds()
	{
		return new Vector3(
		   Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
		   Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
		   transform.position.z
	   );	
	}

}
