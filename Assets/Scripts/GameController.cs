using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	public GameObject hazard;
	public GameObject waterLeft;
	public GameObject waterRight;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start()
	{
		AddWater();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
		StartCoroutine (ScoreAdder());
	}

	void Update ()
	{
		if (restart) 
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves () //ienumerator for asynchronous
	{
		yield return new WaitForSeconds (startWait);
		while(!gameOver)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + 0.35f, spawnValues.x - 0.35f), spawnValues.y, 0.0f);
				Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
		restartText.text = "Press R for Restart";
		restart = true;
	}

	IEnumerator ScoreAdder ()
	{
		while (!gameOver) //TODO: change to as long as player still alive
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

	private void AddWater()
	{
		Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		Vector3 lspawnPosition = new Vector3 (-3.0f, 1.64f, 0.0f);
		Instantiate (waterLeft, lspawnPosition, spawnRotation);
		Vector3 rspawnPosition = new Vector3 (3.0f, 1.64f, 0.0f);
		Instantiate (waterRight, rspawnPosition, spawnRotation);
	}

	//Add score from other actions
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	public void GameOver()
	{
		gameOverText.text = "Game over!";
		gameOver = true;
	}

}
