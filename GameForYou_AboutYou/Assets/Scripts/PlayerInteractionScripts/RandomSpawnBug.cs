using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnBug : MonoBehaviour
{
	public GameObject bugObject;
	float RandX;
	Vector2 spawnPos;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;
	public int counter = 0;

	public GameObject TextObj;
	public GameObject TextObj2;

	RandomSpawnBug RSB;

	void Start()
	{
		RSB = GetComponent<RandomSpawnBug>();
	}

	void Update()
    {
        if(Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
;			RandX = Random.Range(-13.83f, 13.48f);
			spawnPos = new Vector2(RandX, transform.position.y);
			Instantiate(bugObject, spawnPos, Quaternion.identity);
			counter++;
			
		}
		switch(counter)
		{
			case 15:
				spawnRate = 1.5f;
				break;
			case 30:
				spawnRate = 1f;
				break;
			case 50:
				spawnRate = 0.8f;
				break;
			case 70:
				spawnRate = 0.5f;
				break;
			case 90:
				spawnRate = 0.3f;
				break;
		}
		if (counter == 100)
		{
			RSB.enabled = false;
			TextObj.SetActive(true);
			TextObj2.SetActive(false);
		}
	}
}
