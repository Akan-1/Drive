using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLocationTrigger : MonoBehaviour
{
	public GameObject notice;
	public bool inTrigger;
	
	void Update()
	{
		if(inTrigger)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				SceneManager.LoadScene("MiniGame");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.TryGetComponent<Player>(out Player player))
		{
			notice.SetActive(true);
			inTrigger = true;
		}
	}
	void OnTriggerExit2D(Collider2D exit)
	{
		notice.SetActive(false);
	}
}
