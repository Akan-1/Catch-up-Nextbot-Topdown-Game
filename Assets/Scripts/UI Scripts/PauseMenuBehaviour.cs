using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
	#region Fields

	private bool GameOnPaused = false;
	[SerializeField] private GameObject PauseUI;

	#endregion

	#region PauseLogic
	[SerializeField] private GameObject settingsPanel;

	public void Resume()
	{
		GameOnPaused = false;
		Time.timeScale = 1f;
		PauseUI.SetActive(false);
	}
	private void Pause()
	{
		GameOnPaused = true;
		Time.timeScale = 0f;
		PauseUI.SetActive(true);	
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

	public void ResumeGameButton()
	{
		Resume();
	}

}
