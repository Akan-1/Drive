using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 2f;
	public Vector2 moveVector;

	public Rigidbody2D rigidBody;

    void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Walk();
    }

	void Walk()
	{
		moveVector.x = Input.GetAxis("Horizontal");
		rigidBody.velocity = new Vector2(moveVector.x * speed, rigidBody.velocity.y);
	}
}
