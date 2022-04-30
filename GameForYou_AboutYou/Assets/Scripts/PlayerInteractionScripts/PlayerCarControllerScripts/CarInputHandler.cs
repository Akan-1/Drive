using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

	

	public CarMove carMove;

	void Awake()
	{
		carMove = GetComponent<CarMove>();
	}

    void Update()
    {
		Vector2 inputVector = Vector2.zero;

		inputVector.x = Input.GetAxis("Horizontal");
		inputVector.y = Input.GetAxis("Vertical");

		carMove.SetInputVector(inputVector);
    }
}
