using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public Boundary boundary;

	public GameController gameController;
	
	void Start()
	{
		//find the game controller object
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveHorizontal = Input.acceleration.x * 2;
		//float moveVertical = Input.GetAxis ("Vertical");
		//float moveVertical = Input.acceleration.y * 2;

		Vector2 movement = new Vector2 (moveHorizontal, 0.0f);
		rigidbody2D.velocity = movement * speed;

		rigidbody2D.transform.position = new Vector2 (Mathf.Clamp (rigidbody2D.transform.position.x, boundary.xMin + gameController.WaterDistance, boundary.xMax - gameController.WaterDistance), Mathf.Clamp (rigidbody2D.transform.position.y, boundary.yMin, boundary.yMax));
	
	}

}
