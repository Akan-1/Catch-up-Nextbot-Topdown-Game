using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	#region Player Config
	[Header("Player config")]
	[SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _runMovementSpeed;
    [Header("Sprint")]
    [SerializeField] private float _stamina;
    [SerializeField] private float _staminaReduction;
    [SerializeField] private float _staminaMinimumValue;
    
    private float _defaultStaminaValue;

    private CameraBehaviour _camera;
	#endregion

	#region Private Fields
	private bool _canUseStamina = true;
    private float _currentMovementSpeed;
    private Vector2 _direction;
    private Rigidbody2D rb;
    private PauseMenuBehaviour _pause;
	#endregion

	[Header("Tab Button Config")]
    [SerializeField] private UnityEvent _onEnableEvent;
    [SerializeField] private UnityEvent _onDisableEvent;
    private bool _canUsePicker = true;

    public void CanUserPicker(bool value) => _canUsePicker = value;

    [Header("UI")] 
    [SerializeField] private Image _staminaBar;

    private bool _didEventActivatedBefore;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _pause = FindObjectOfType<PauseMenuBehaviour>();
        _currentMovementSpeed = _defaultMovementSpeed;
        _defaultStaminaValue = _stamina;
        _camera = Camera.main.GetComponent<CameraBehaviour>();
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

	#region Player Methods
	private void Walk()
	{
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + _direction * _currentMovementSpeed * Time.fixedDeltaTime);

    }

    private void CheckPressButton()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && _canUsePicker)
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
	    if (Input.GetKey(KeyCode.LeftShift) && _stamina > 0f && _canUseStamina && (_direction.x != 0 || _direction.y != 0))
		{
			_staminaBar.gameObject.SetActive(true);
			_currentMovementSpeed = _runMovementSpeed;
            _stamina -= _staminaReduction;
            _staminaBar.fillAmount = _stamina / _defaultStaminaValue;
            _camera.cameraIncrease = true;
		}
        else
		{
			_currentMovementSpeed = _defaultMovementSpeed;
            if (_stamina < _defaultStaminaValue)
            {
	            if(_pause.GameOnPaused == false)
					_stamina += _staminaReduction;
	            _staminaBar.fillAmount = _stamina / _defaultStaminaValue;
	            _canUseStamina = _stamina >= _staminaMinimumValue;
            }
            else
            {
	            _staminaBar.gameObject.SetActive(false);
            }
            _camera.cameraIncrease = false;
		}

	    _staminaBar.color = _stamina >= _staminaMinimumValue ? Color.cyan : Color.red;
    }

	private void LookAtMouse()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.up = (mousePos - new Vector2(transform.position.x, transform.position.y));
	}
	#endregion
}
