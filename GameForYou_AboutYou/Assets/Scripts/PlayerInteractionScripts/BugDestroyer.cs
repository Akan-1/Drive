using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BugDestroyer : MonoBehaviour
{
	public Animator anim;
	public AudioSource sound1;
	public AudioSource sound2;

	int num;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnMouseDown()
	{
		anim.SetTrigger("death");
		num = Random.Range(1, 3);
		if (num == 1)
			sound1.Play();
		if (num == 2)
			sound2.Play();
		
		Destroy(gameObject, 0.5f);
	}
}
