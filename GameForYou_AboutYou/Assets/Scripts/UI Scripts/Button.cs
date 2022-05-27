using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour
{
    public void ToRoadLocationButton()
	{
		SceneManager.LoadScene("Road");
	}

	public void ToOfficeLocationButton()
	{
		SceneManager.LoadScene("Office");
	}

	public void ToCreditsButton()
	{
		SceneManager.LoadScene("Credits");
	}

	public void ToMainMenuButton()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ExitButton()
	{
		Application.Quit();
	}
}
