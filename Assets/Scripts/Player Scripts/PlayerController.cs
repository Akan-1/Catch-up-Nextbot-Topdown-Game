using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player config")]
	[SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _runMovementSpeed;
    private float _currentMovementSpeed;

	private Vector2 _direction;
    private Rigidbody2D rb;

	private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        _currentMovementSpeed = _defaultMovementSpeed;
    }

	private void Update()
	{
        LookAtMouse();
        Sprint();

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
