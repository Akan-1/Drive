using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	#region Fields

	private bool GameOnPaused = false;
	public GameObject PauseUI;

	#endregion

	#region PauseLogic

	void Resume()
	{
		GameOnPaused = false;
		Time.timeScale = 1f;
		PauseUI.SetActive(false);
		AudioListener.pause = false;
	}
	void Pause()
	{
		GameOnPaused = true;
		Time.timeScale = 0f;
		PauseUI.SetActive(true);
		AudioListener.pause = true;
	}

	void Start()
	{
		Resume();
	}
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(GameOnPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}	
    }
	
	#endregion

	#region Buttons

	public void ResumeGameButton()
	{
		Resume();
	}

	public void ExitToMainMenuButton()
	{
		SceneManager.LoadScene("MainMenu");
		AudioListener.pause = false;
	}
	
	#endregion
}
