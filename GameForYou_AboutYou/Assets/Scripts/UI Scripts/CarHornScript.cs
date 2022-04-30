using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHornScript : MonoBehaviour
{
	public AudioSource[] musicArray = {};

	void OnMouseDown()
	{
		musicArray[Random.Range(1, 2)].Play();	
	}
}
