using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Player config")]
	[SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _runMovementSpeed;
    private float _currentMovementSpeed;

	private Vector2 _direction;
    private Rigidbody2D rb;

    [Header("Tab ")]
    [SerializeField] private UnityEvent _onEnableEvent;
    [SerializeField] private UnityEvent _onDisableEvent;

    private bool _didEventActivatedBefore = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentMovementSpeed = _defaultMovementSpeed;
    }

	private void Update()
	{
        LookAtMouse();
        Sprint();
        CheckPressButton();
    }

	private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
	{
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + _direction * _currentMovementSpeed * Time.fixedDeltaTime);
    }

    private void CheckPressButton()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
            _didEventActivatedBefore = !_didEventActivatedBefore;

			if (_didEventActivatedBefore)
			{
                _onEnableEvent.Invoke();
			}
			else
			{
                _onDisableEvent.Invoke();
			}

        }
	}

    private void Sprint()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
            _currentMovementSpeed = _runMovementSpeed;
		}
        else
		{
            _currentMovementSpeed = _defaultMovementSpeed;
		}
	}

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (mousePos - new Vector2(transform.position.x, transform.position.y));
    }
}
