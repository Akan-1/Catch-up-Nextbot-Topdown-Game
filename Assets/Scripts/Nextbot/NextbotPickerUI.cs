using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using System.Collections;

public class NextbotPickerUI : MonoBehaviour
{
	[SerializeField] private List<Transform> _spawnPointsList;
	private TextMeshProUGUI _nextbotName;

	public NextbotController nextbotController;
	public static Action<NextbotController> onNextbotSpawn;

	private void Start()
	{
		GameObject spawnpointsObj = GameObject.Find("Spawnpoints");
		Transform[] spawnPoints = spawnpointsObj.GetComponentsInChildren<Transform>();

		foreach (Transform spawnPoint in spawnPoints)
		{
			if (spawnPoint != spawnpointsObj.transform)
			{
				_spawnPointsList.Add(spawnPoint);
			}
		}
		Debug.Log(_spawnPointsList.Count);

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
		int spawnPointIndex = Rand.Range(1, _spawnPointsList.Count - 1);
		Transform spawnPoint = _spawnPointsList[spawnPointIndex];

		GameObject nextbotObject = Instantiate(this.nextbotController.gameObject, spawnPoint.position, Quaternion.identity);
		NextbotController nextbotController = nextbotObject.GetComponent<NextbotController>();

		onNextbotSpawn?.Invoke(nextbotController);
	}
}
