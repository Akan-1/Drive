using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 2f;
	public Vector2 moveVector;
	Animator animator;
	bool faceRight = true;
	Rigidbody2D rigidBody;

    void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

    
    void Update()
    { 
        Walk();
		Reflect();
	}

	void Walk()
	{
		moveVector.x = Input.GetAxis("Horizontal");
		animator.SetFloat("Speed", Mathf.Abs(moveVector.x));
		rigidBody.velocity = new Vector2(moveVector.x * speed, rigidBody.velocity.y);
	}

	void Reflect()
    {
		if((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
			transform.localScale *= new Vector2(-1, 1);
			faceRight = !faceRight;
        }
    }
}
