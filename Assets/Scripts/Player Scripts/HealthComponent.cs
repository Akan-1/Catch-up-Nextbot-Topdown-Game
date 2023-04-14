using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamagable
{
	[Header("Health")]
	[SerializeField] private int _defaultHealthAmount;
	[SerializeField] private int _criticalHealthAmount;

	public int Health
	{
		get => _defaultHealthAmount;
		set => _defaultHealthAmount = value;
	}

	public int MinimumHealth
	{
		get => _criticalHealthAmount;
		set => _criticalHealthAmount = value;
	}

	public UnityEvent OnDie;

	public void TakeDamage(int damage, bool isCanAttack)
	{
		if (isCanAttack)
		{
			_defaultHealthAmount -= damage;
		}

		if (_defaultHealthAmount <= 0)
		{
			_defaultHealthAmount = 0;
			OnDie?.Invoke();
		}
	}
}
