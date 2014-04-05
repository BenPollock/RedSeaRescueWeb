using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	private int score;

	void Start()
	{
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
		StartCoroutine (ScoreAdder ());
	}

	IEnumerator SpawnWaves () //ienumerator for asynchronous
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, 0.0f);
				Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	IEnumerator ScoreAdder ()
	{
		while (true) //TODO: change to as long as player still alive
		{
			yield return new WaitForSeconds (0.1f);
			score += 100;
			UpdateScore ();
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	//Add score from other actions
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

}
