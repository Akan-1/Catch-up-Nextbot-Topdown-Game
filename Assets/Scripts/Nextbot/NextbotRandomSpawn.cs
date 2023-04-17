using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextbotRandomSpawn : MonoBehaviour
{
	List<Transform> _spawnPointsList;

	private void Start()
    {
        List<NextbotController> nextbotsList = Resources.LoadAll<NextbotController>("Nextbots").ToList();

		GameObject spawnpointsObj = GameObject.Find("Spawnpoints");
		Transform[] spawnPoints = spawnpointsObj.GetComponentsInChildren<Transform>();

		_spawnPointsList = new List<Transform>(); 

		foreach (Transform spawnPoint in spawnPoints)
		{
			if (spawnPoint != spawnpointsObj.transform)
			{
				_spawnPointsList.Add(spawnPoint);
			}
		}

		int spawnPointIndex = Random.Range(1, _spawnPointsList.Count - 1);
		Transform spawnPointsPos = _spawnPointsList[spawnPointIndex];

		Instantiate(nextbotsList[Random.Range(0, nextbotsList.Count)],
			spawnPointsPos.position, Quaternion.identity);
	}
}
