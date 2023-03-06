using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUIComponent : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _healthAmountText;
	[SerializeField] private Image _backPanel;

	private HealthComponent _playerLifes;

	private void Awake()
	{
		_playerLifes = GetComponentInParent<HealthComponent>();
	}

	private void Update()
	{
		UpdateLifeUI();
	}

	private void UpdateLifeUI()
	{
		_healthAmountText.text = _playerLifes.Health.ToString();

		if(_playerLifes.Health <= _playerLifes.MinimumHealth)
		{
			_healthAmountText.color = new Color(0.8113208f, 0.06917161f, 0.01148093f, 0.7098039f);
			_backPanel.color = new Color(0.9333333f, 0.5941117f, 0.5764706f, 0.3921569f);
		}
		else
		{
			_healthAmountText.color = Color.white;
			_backPanel.color = new Color(0.9339623f, 0.7655349f, 0.5771182f, 0.3921569f);
		}
	}

}
