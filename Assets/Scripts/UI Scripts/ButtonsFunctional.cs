using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunctional : MonoBehaviour
{
	[SerializeField] private GameObject _asyncLoadPanel;
	[SerializeField] private Image _asyncLoadBar;
	[SerializeField] private TMP_Text _anyKeyText;
	
	public void LoadScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
	
	public void AsyncLoadScene(int sceneIndex)
	{
		StartCoroutine(AsyncLoadSceneCoroutine(sceneIndex));
	}
	
	public void AsyncRandomLoadScene()
	{
		StartCoroutine(AsyncLoadSceneCoroutine(Random.Range(1, SceneManager.sceneCountInBuildSettings + 1)));
	}

	private IEnumerator AsyncLoadSceneCoroutine(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		operation.allowSceneActivation = false;
		_asyncLoadPanel.SetActive(true);
		
		while (!operation.isDone)
		{
			_asyncLoadBar.fillAmount = operation.progress / 1;
			if (operation.progress >= .9f)
			{
				_asyncLoadBar.fillAmount = 1;
				_anyKeyText.gameObject.SetActive(true);
				operation.allowSceneActivation = true;
				break;
			}
			yield return null;
		}

		yield return new WaitUntil(() => Input.anyKeyDown);
	}

	
	public void CloseGame()
	{
		Application.Quit();
	}

}
