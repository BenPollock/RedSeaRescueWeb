using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Boundary") {
			return;
		}
		//	gameController.AddScore (1000000);
		Destroy (other.gameObject); //destroy player, we'll fix this later
		Destroy (gameObject);  //destroy itself
		gameController.GameOver ();
	}
}
