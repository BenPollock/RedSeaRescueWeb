using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{

	public float horizontal_speed;
	public float vertical_speed;

	void Start()
	{
		rigidbody2D.velocity = new Vector2 (horizontal_speed, vertical_speed);
	}

}
