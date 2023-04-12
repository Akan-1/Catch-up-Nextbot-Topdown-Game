using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class NextbotController : MonoBehaviour
{
	[Tooltip("The target is found automatically when game started")]
	[SerializeField] private Transform _target;
	[SerializeField] private AudioClip _music;
	[SerializeField] private int _damage;

	private bool _isFaceRight;
	private bool _isCanAttack = true;
	private bool _isCooldownEnded = true;
	private bool _isTargetNearby;

	private NavMeshAgent _agent;
	private AudioSource _audioSource;

	private void Awake()
	{
		_target = FindObjectOfType<PlayerController>().transform;

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_agent.velocity = Vector3.zero;

		_audioSource = GetComponent<AudioSource>();
		_audioSource.loop = true;
		_audioSource.spread = 360f;
		_audioSource.dopplerLevel = 0;
		_audioSource.rolloffMode = AudioRolloffMode.Linear;
		_audioSource.maxDistance = 75f;
		_audioSource.spatialBlend = 1f;
		_audioSource.panStereo = 1f;
	}

	private void Start()
	{
		_audioSource.clip = _music;
		_audioSource.Play();
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
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out IDamagable damagableObject))
		{
			_isTargetNearby = true;
			StartCoroutine(StartTakeDamage(damagableObject));
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out IDamagable damagableObject))
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
