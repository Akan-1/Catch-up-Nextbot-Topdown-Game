using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunctional : MonoBehaviour
{
	[SerializeField] private GameObject _asyncLoadPanel;
	[SerializeField] private Image _asyncLoadBar;
	
	public void LoadScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
	
	public void AsyncLoadScene(int sceneIndex)
	{
		StartCoroutine(AsyncLoadSceneCoroutine(sceneIndex));
	}

	private IEnumerator AsyncLoadSceneCoroutine(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		_asyncLoadPanel.SetActive(true);
		
		while (!operation.isDone)
		{
			float progress = operation.progress;
			_asyncLoadBar.fillAmount = progress / 1;
			yield return null;
		}
	}

	
	public void CloseGame()
	{
		Application.Quit();
	}

}
