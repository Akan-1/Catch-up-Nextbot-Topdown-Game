using System.Collections.Generic;
using UnityEngine;

public class NextbotsVault : MonoBehaviour
{

	[SerializeField] private List<NextbotController> _nextbotList;

	private void OnEnable()
	{
		NextbotPickerUI.onNextbotSpawn += AddNextbotToList;
	}

	private void OnDisable()
	{
		NextbotPickerUI.onNextbotSpawn -= AddNextbotToList;
	}

	public void AddNextbotToList(NextbotController nextbot)
	{
		_nextbotList.Add(nextbot);
	}

	public void ClearAllNextbots()
	{
		for (int i = _nextbotList.Count - 1; i >= 0; i--)
		{
			Destroy(_nextbotList[i].gameObject);
			_nextbotList.RemoveAt(i);
		}
	}

	public void RemoveLastNextbot()
	{
		if (_nextbotList.Count > 0)
		{
			NextbotController lastNextbot = _nextbotList[_nextbotList.Count - 1];
			if (lastNextbot != null)
			{
				Destroy(lastNextbot.gameObject);
			}
			_nextbotList.RemoveAt(_nextbotList.Count - 1); 
		}
	}
}
