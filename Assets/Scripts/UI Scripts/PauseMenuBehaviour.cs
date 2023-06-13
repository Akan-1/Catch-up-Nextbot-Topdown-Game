using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
	#region Fields

	public bool GameOnPaused = false;
	[SerializeField] private GameObject PauseUI;

	#endregion

	#region PauseLogic
	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject _basicButtons;

	public void Resume()
	{
		GameOnPaused = false;
		Time.timeScale = 1f;
		PauseUI.SetActive(GameOnPaused);
	}
	private void Pause()
	{
		_basicButtons.SetActive(true);
		GameOnPaused = true;
		Time.timeScale = 0f;
		PauseUI.SetActive(GameOnPaused);	
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void Start()
	{
		Resume();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameOnPaused)
			{
				Resume();
				settingsPanel.SetActive(false);
			}
			else
			{
				Pause();
			}
		}
	}

	#endregion

	public void SetTimeScale(float scale)
	{
		Time.timeScale = scale;
	}

	public void ResumeGameButton()
	{
		Resume();
	}

}
