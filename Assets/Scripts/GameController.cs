using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	public GameObject hazard;
	public GameObject waterLeft;
	public GameObject waterRight;

	private GameObject _waterLeft;
	private GameObject _waterRight;

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

	public float waterDistance;

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
			//if (Input.GetKeyDown(KeyCode.R))
			//{
				Application.LoadLevel ("splash");
		//	}
		}
	}

	IEnumerator SpawnWaves () //ienumerator for asynchronous
	{
		yield return new WaitForSeconds (startWait);
		while(!gameOver)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
				Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
				Instantiate(hazard, spawnPosition, spawnRotation);

				Vector3 leftPosition = _waterLeft.transform.position;
				leftPosition.x += 0.01f;
				Vector3 rightPosition = _waterRight.transform.position;
				rightPosition.x -= 0.01f;
				_waterLeft.transform.position = leftPosition;
				_waterRight.transform.position = rightPosition;
				waterDistance += 0.01f;

				yield return new WaitForSeconds(spawnWait);
			}
			//Successfully beat the level
			yield return new WaitForSeconds(waveWait);
		}
		restartText.text = "Press R for Restart";
		restart = true;
	}

	IEnumerator ScoreAdder ()
	{
		while (!gameOver)
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
		_waterLeft = (GameObject) Instantiate (waterLeft, lspawnPosition, spawnRotation);
		Vector3 rspawnPosition = new Vector3 (3.0f, 1.64f, 0.0f);
		_waterRight = (GameObject) Instantiate (waterRight, rspawnPosition, spawnRotation);
		//waterRight.transform.position
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
	public float WaterDistance{
		get
		{
			return waterDistance;
		}
	}

}
