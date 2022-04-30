using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimations : MonoBehaviour
{
	public Text TextGameObject;
	private string text;

	private void Start()
	{
		text = TextGameObject.text;
		TextGameObject.text = "";
		StartCoroutine(TextCorutine());
	}


	IEnumerator TextCorutine()
	{
		foreach (char abc in text)
		{
			TextGameObject.text += abc;
			yield return new WaitForSeconds(0.09f);
		}
	}
}
