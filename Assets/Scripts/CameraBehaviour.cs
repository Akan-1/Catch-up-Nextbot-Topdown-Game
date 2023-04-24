using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private bool _isUsingBounds;
	[Space]
	[SerializeField] private float _cameraSizeIncreaseRate = 3.5f;
	[SerializeField] private float _cameraSizeDecreaseRate = 4f;
	[SerializeField] private float _maxCameraSize = 17.0f;
	private float _originalCameraSize; // Original size of the camera
	[HideInInspector] public bool cameraIncrease;

	private Bounds _cameraBounds;
	private Vector3 _targetPosition;
	private Camera _main;

	private void Start()
	{
		_main = Camera.main;
		_originalCameraSize = _main.orthographicSize;
		if (_isUsingBounds)
		{
			SetCameraBounds();
		}
	}

	public void SetCameraBounds()
	{
		var height = _main.orthographicSize;
		var width = height * _main.aspect;

		var screenBounds = new Bounds(Vector3.zero, new Vector3(Screen.width, Screen.height, 0));
		screenBounds.center = new Vector2(0, 0);
		screenBounds.size /= _main.orthographicSize * 2.0f;

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
		
		if (cameraIncrease)
		{
			if (_main.orthographicSize < _maxCameraSize)
			{
				_main.orthographicSize += _cameraSizeIncreaseRate * Time.deltaTime;
			}
		}
		else
		{
			if (_main.orthographicSize > _originalCameraSize)
			{
				_main.orthographicSize -= _cameraSizeDecreaseRate * Time.deltaTime;
			}
		}

		if (_isUsingBounds)
		{
			SetCameraBounds();
			_targetPosition = smoothedPosition;
			_targetPosition = GetCameraBounds();
			transform.position = _targetPosition;
		}
		else
		{
			transform.position = smoothedPosition;
		}

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
