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
    private Animator _animator;
    #endregion

    [Header("Attack")]
    [SerializeField] private float _defaultAttackDelay = 0.3f;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _radius;
    [SerializeField] private Transform _attackPos;
    [SerializeField] private LayerMask _interactableObjectMask;
    private float _attackDelay;
    
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
        _animator = GetComponent<Animator>();
    }

	private void Update()
	{
        LookAtMouse();
        Sprint();
        CheckPressButton();
        Attack();
    }

    private void FixedUpdate()
    {
        Walk();
    }

	#region Player Methods
	private void Walk()
	{
        bool isMoving = false;
        
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(_direction.x, _direction.y).normalized;

        if (movement != Vector2.zero)
            isMoving = true;

        _animator.SetBool("isMoving", isMoving);


        rb.MovePosition(rb.position + movement * _currentMovementSpeed * Time.fixedDeltaTime);

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

    private void Attack()
	{
        if (_defaultAttackDelay <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] hitObj = Physics2D.OverlapCircleAll(_attackPos.position, _radius, _interactableObjectMask);

                foreach(Collider2D obj in hitObj)
				{
                    obj.gameObject.GetComponent<HealthComponent>().TakeDamage(_attackDamage);
				}

                _defaultAttackDelay = _attackDelay;
            }
        }
		else
		{
            _defaultAttackDelay -= Time.deltaTime;
		}
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPos.position, _radius);
    }

    #endregion
}
