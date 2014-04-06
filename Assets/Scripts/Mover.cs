using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{

	public float speed;

	void Start()
	{
		rigidbody2D.velocity = new Vector2 (0.0f, speed);
	}

	public float Speed 
	{
		get{
			return speed;
		}
		set{
			speed = value;
		}
	}
}
