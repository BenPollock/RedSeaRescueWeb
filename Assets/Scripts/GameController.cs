using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour 
{

	public GameObject hazard;
	public GameObject coral;
	public GameObject waterLeft;
	public GameObject waterRight;
	public GameObject trench;
	public GameObject fishLeft;
	public GameObject fishRight;

	private GameObject _waterLeft;
	private GameObject _waterRight;

	public Vector3 spawnValues;
	public int hazardCount;
	public float maxSpawnWait;
	public float minSpawnWait;
	public float startWait;
	public float waveWait;
	public int level;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText levelText;

	private bool gameOver;
	private bool restart;
	private int score;
	private float speedAdder = 0f;

	public AudioClip splash;
	public AudioSource mainAudio;

	public float waterDistance;

	void Start()
	{
		AddWater();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		scoreText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 20f);
		levelText.text = "";
		levelText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 10f);
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
			//Disabling Waves for now
			//for (int i = 0; i < hazardCount; i++)
			//

				//Spawn fish enemies
				if(level >= 5){
					int spawnRange = Random.Range (0,4);
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
					Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
					if(spawnRange == 0){
						Instantiate(hazard, spawnPosition, spawnRotation);
						
					}else if (spawnRange == 1){
						Instantiate (coral, spawnPosition, spawnRotation);
					}
					else if (spawnRange == 2){
						Instantiate(trench, spawnPosition, spawnRotation);
					}
					else{
						spawnRange = Random.Range(0,2);
						if(spawnRange == 0){
							spawnPosition = new Vector3 (-2f, 3.5f, 0.0f);
							Instantiate(fishLeft, spawnPosition, spawnRotation);
						}else{
							spawnPosition = new Vector3 (2f, 3.5f, 0.0f);
							Instantiate(fishRight, spawnPosition, spawnRotation);
						}
						mainAudio.PlayOneShot(splash, 1.0f);
					}
					
				}
				//Spawn ocean trenches
				else if(level >= 3){
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
					Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
					int spawnRange = Random.Range(0,3);
					if(spawnRange == 0){
						Instantiate(hazard, spawnPosition, spawnRotation);
						
					}else if (spawnRange == 1){
						Instantiate (coral, spawnPosition, spawnRotation);
					}
					else{
						Instantiate(trench, spawnPosition, spawnRotation);
					}

			}
				else
				{
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
					Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);

					//Spawn either coral or boulder
					if(Random.Range(0,2) == 1){
						Instantiate(hazard, spawnPosition, spawnRotation);
					}
					else{
						Instantiate(coral, spawnPosition, spawnRotation);
					}
				}
			/*
				Vector3 leftPosition = _waterLeft.transform.position;
				leftPosition.x += 0.01f;
				Vector3 rightPosition = _waterRight.transform.position;
				rightPosition.x -= 0.01f;
				_waterLeft.transform.position = leftPosition;
				_waterRight.transform.position = rightPosition;
				waterDistance += 0.01f;

			*/

				yield return new WaitForSeconds(Random.Range (minSpawnWait, maxSpawnWait));
			//}
			//Successfully beat the level
			//yield return new WaitForSeconds(waveWait);
		}
		restartText.text = "Press R for Restart";
		restart = true;
	}

	IEnumerator ScoreAdder ()
	{
		while (!gameOver)
		{
			yield return new WaitForSeconds (0.5f);
			score += 1;
			UpdateScore ();
			if(score % 15 == 0)
			{
				level++;

				//each level enemies spawn faster, to a limit
				//if(minSpawnWait > 0.2f)
			//		minSpawnWait -= 0.1f;
				if(maxSpawnWait > 1f)
					maxSpawnWait -= 0.1f;
				

				//increase water every other level, to a limit
				if(level % 2 == 0 && level < 10)
				{
					Vector3 leftPosition = _waterLeft.transform.position;
					leftPosition.x += 0.1f;
					Vector3 rightPosition = _waterRight.transform.position;
					rightPosition.x -= 0.1f;
					_waterLeft.transform.position = leftPosition;
					_waterRight.transform.position = rightPosition;
					waterDistance += 0.1f;
				}else{
				//Increase speed every other level
					speedAdder += -1.0f;
				}
				StartCoroutine(ShowLevelUpdate());
			}
		}
	}

	IEnumerator ShowLevelUpdate()
	{
		levelText.text = "Level " + level;
		yield return new WaitForSeconds (2f);
		levelText.text = "";
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	private void AddWater()
	{
		Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		Vector3 lspawnPosition = new Vector3 (-6.75f, 1.64f, 0.0f);
		_waterLeft = (GameObject) Instantiate (waterLeft, lspawnPosition, spawnRotation);
		Vector3 rspawnPosition = new Vector3 (6.75f, 1.64f, 0.0f);
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
