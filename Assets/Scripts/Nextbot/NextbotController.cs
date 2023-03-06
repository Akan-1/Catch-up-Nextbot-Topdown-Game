using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NextbotController : MonoBehaviour
{
	[Tooltip("The target is found automatically when game started")]
	[SerializeField] private Transform _target;
	
	[SerializeField] private int _damage;

	private bool _isFaceRight;
	private bool _isCanAttack = true;
	private bool _isCooldownEnded = true;
	private bool _isTargetNearby = false;

	private NavMeshAgent _agent;

	private void Awake()
	{
		_target = FindObjectOfType<PlayerController>().transform;

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;

		//_agent.speed = _movementSpeed;
	}

	private void Update()
	{
		StalkingPlayer();
		Flip();
	}

	private void StalkingPlayer()
	{
		_agent.SetDestination(_target.position);
	}

	private void Flip()
	{
		if ((_target.position.x < transform.position.x && _isFaceRight) || (_target.position.x > transform.position.x && !_isFaceRight))
		{
			transform.localScale *= new Vector2(-1, 1);
			_isFaceRight = !_isFaceRight;
		}
	}

	#region Coroutines
	private IEnumerator StartTakeDamage(IDamagable target)
	{
		while (_isTargetNearby)
		{
			target.TakeDamage(_damage, _isCanAttack);
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator TakeDamageCooldownCoroutine()
	{
		_isCooldownEnded = false;
		_isCanAttack = false;
		yield return new WaitForSeconds(1f);
		_isCanAttack = true;
		_isCooldownEnded = true;
	}
	#endregion

	#region OnCollision Stuff
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out IDamagable damagableObject))
		{
			_isTargetNearby = true;
			StartCoroutine(StartTakeDamage(damagableObject));
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out IDamagable damagableObject))
		{
			_isTargetNearby = false;
			
			if (_isCooldownEnded)
			{
				StartCoroutine(TakeDamageCooldownCoroutine());
			}

			StopCoroutine(StartTakeDamage(damagableObject));
		}
	}
	#endregion
}
