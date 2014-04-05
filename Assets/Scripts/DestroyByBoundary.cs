using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log ("destroy " + other.gameObject.name);
		Destroy (other.gameObject);
	}

}
