using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NextbotPickerUI : MonoBehaviour
{
	private Transform _spawnPoint;
	private Image _nextbotImage;
	private TextMeshProUGUI _nextbotName;

	public NextbotController nextbotController;
	public static Action<NextbotController> _onNextbotSpawn;

	private void Start()
	{
		_spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform;

		_nextbotName = GetComponentInChildren<TextMeshProUGUI>();
		_nextbotName.text = nextbotController.name;

		if (nextbotController.name.Length > 10)
		{
			_nextbotName.fontSize = 15;
		}

		GetComponent<Image>().sprite = nextbotController.GetComponent<SpriteRenderer>().sprite;
	}
	

	public void AddNextbotToMap()
	{
		GameObject nextbotObject = Instantiate(this.nextbotController.gameObject, _spawnPoint.position, Quaternion.identity);
		NextbotController nextbotController = nextbotObject.GetComponent<NextbotController>();

		_onNextbotSpawn?.Invoke(nextbotController);
	}
}
