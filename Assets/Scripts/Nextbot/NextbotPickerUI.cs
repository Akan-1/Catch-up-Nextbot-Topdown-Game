using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NextbotPickerUI : MonoBehaviour
{

	private Transform _spawnPoint;
	private TextMeshProUGUI _nextbotName;

	[SerializeField] private NextbotController _nextbotController;

	public static Action<NextbotController> _onNextbotSpawn;

	private void Awake()
	{
		_spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform;

		_nextbotName = GetComponentInChildren<TextMeshProUGUI>();
		_nextbotName.text = _nextbotController.name;

		if (_nextbotController.name.Length > 10)
		{
			_nextbotName.fontSize = 15;
		}

		GetComponent<Image>().sprite = _nextbotController.GetComponent<SpriteRenderer>().sprite;
	}

	public void AddNextbotToMap()
	{
		GameObject nextbotObject = Instantiate(_nextbotController.gameObject, _spawnPoint.position, Quaternion.identity);
		NextbotController nextbotController = nextbotObject.GetComponent<NextbotController>();

		_onNextbotSpawn?.Invoke(nextbotController);
	}
}
