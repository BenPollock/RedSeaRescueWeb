using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

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

	public GameObject gameOverBox;
	private GameObject _gameOverBox;

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
	private GameObject mainAudio;

	public float waterDistance;
	private int highscore = 0;

	public Texture replay;
	public Texture quit;
	public Texture leaderboard;
	public Texture achievements;

	private bool goliathMode = false;
	private bool hasAuthenticated = false;
	private bool socialHasFailed = false;

	void Awake()
	{
		mainAudio = GameObject.Find ("Game Music");
	}

	void Start()
	{
		AddWater();
		gameOver = false;
		restart = false;
		restartText.text = "Max: 0";
		gameOverText.text = "";
		scoreText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 20f);
		restartText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 20f);
		levelText.text = "";
		levelText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 10f);
		gameOverText.fontSize = (int) (Mathf.Min (Screen.width, Screen.height) / 15f);
		score = 0;
		UpdateScore ();

		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();

		StartCoroutine (ShowInstructions ());
		StartCoroutine (SpawnWaves());
		StartCoroutine (ScoreAdder());

		//Prevent phone from sleeping
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Get highscore
		if (PlayerPrefs.HasKey ("Highscore")) {
			highscore = PlayerPrefs.GetInt("Highscore");
			restartText.text = "Max: " + highscore;
		}

		//Goliath mode?
	/*	if (PlayerPrefs.HasKey ("Goliath")) {
			if(PlayerPrefs.GetInt ("Goliath") == 1)
				goliathMode = true;
		}*/


	}

	void Update ()
	{
		if (restart) 
		{
			//if (Input.GetKeyDown(KeyCode.R))
			//{
			//Destroy (_gameOverBox);
				//Application.LoadLevel ("splash");
		//	}
		}
	}

	IEnumerator ShowInstructions()
	{
		gameOverText.text = "Tilt to Dodge";
		yield return new WaitForSeconds(1.3f);
		gameOverText.text = "";
	}

	IEnumerator SpawnWaves () //ienumerator for asynchronous
	{
		yield return new WaitForSeconds (startWait);
		while(!gameOver)
		{
			//Disabling Waves for now
			//for (int i = 0; i < hazardCount; i++)
			//
				//spawn fish and trenches at same time
				if (level >= 10){
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
					Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
					int spawnRange = Random.Range(0,2);
					if(spawnRange == 0){
						Instantiate(trench, spawnPosition, spawnRotation);
						
					}else if (spawnRange == 1){
						spawnRange = Random.Range(0,2);
						if(spawnRange == 0){
							Instantiate(trench, spawnPosition, spawnRotation);
							spawnPosition = new Vector3 (-2f, 3.66f, 0.0f);
							Instantiate(fishLeft, spawnPosition, spawnRotation);
						}else{
							Instantiate(trench, spawnPosition, spawnRotation);
							spawnPosition = new Vector3 (2f, 3.66f, 0.0f);
							Instantiate(fishRight, spawnPosition, spawnRotation);
						}
						mainAudio.audio.PlayOneShot(splash, 1.0f);
					}
				}
				//More fish and trenches
				else if(level >= 7){
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x + waterDistance, spawnValues.x - waterDistance), spawnValues.y, 0.0f);
					Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
					int spawnRange = Random.Range(0,2);
					if(spawnRange == 0){
						Instantiate(trench, spawnPosition, spawnRotation);
					
					}else if (spawnRange == 1){
						spawnRange = Random.Range(0,2);
						if(spawnRange == 0){
							spawnPosition = new Vector3 (-2f, 3.66f, 0.0f);
							Instantiate(fishLeft, spawnPosition, spawnRotation);
						}else{
							spawnPosition = new Vector3 (2f, 3.66f, 0.0f);
							Instantiate(fishRight, spawnPosition, spawnRotation);
						}
						mainAudio.audio.PlayOneShot(splash, 1.0f);
					}
				}
				//Spawn fish enemies
				else if(level >= 5){
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
							spawnPosition = new Vector3 (-2f, 3.66f, 0.0f);
							Instantiate(fishLeft, spawnPosition, spawnRotation);
						}else{
							spawnPosition = new Vector3 (2f, 3.66f, 0.0f);
							Instantiate(fishRight, spawnPosition, spawnRotation);
						}
						mainAudio.audio.PlayOneShot(splash, 1.0f);
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
		//restartText.text = "Press R for Restart";
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
				if(maxSpawnWait > 1.3f)
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
		yield return new WaitForSeconds (1.5f);
		levelText.text = "";
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	private void AddWater()
	{
		Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		Vector3 lspawnPosition = new Vector3 (-6.65f, 1.64f, 0.0f);
		_waterLeft = (GameObject) Instantiate (waterLeft, lspawnPosition, spawnRotation);
		Vector3 rspawnPosition = new Vector3 (6.65f, 1.64f, 0.0f);
		_waterRight = (GameObject) Instantiate (waterRight, rspawnPosition, spawnRotation);
		//waterRight.transform.position
	}

	//Add score from other actions
	public void AddScore(int newScoreValue)
	{
		if (!gameOver) {
			score += newScoreValue;
			UpdateScore ();
		}
	}
	public void GameOver()
	{
		//gameOverText.text = "Game over!";
		gameOver = true;

		//Set highscore, if not goliath mode
		if (PlayerPrefs.HasKey ("Highscore")) {
			highscore = PlayerPrefs.GetInt("Highscore");
			if(highscore < score && !goliathMode){
				PlayerPrefs.SetInt ("Highscore", score);
			}
		}else if (!goliathMode){
			PlayerPrefs.SetInt("Highscore", score);
		}

		//Create game over popup
		Quaternion spawnRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		Vector3 spawnPosition = new Vector3 (0f, 1.51f, 0.0f);
		_gameOverBox = (GameObject)Instantiate (gameOverBox, spawnPosition, spawnRotation);

		AdMobPlugin.ShowBannerView ();



	}
	public float WaterDistance{
		get
		{
			return waterDistance;
		}
	}

	void ProcessAuthentication(bool success){
		if (success) {
			Debug.Log ("Authentication success");
			hasAuthenticated = true;
			socialHasFailed = false;
		}else{
			Debug.LogError ("Was not success");
			socialHasFailed = true;
		}
	}


	void OnGUI(){
		if (gameOver) {
			if(!hasAuthenticated && !socialHasFailed)
				Social.localUser.Authenticate (ProcessAuthentication);
			//Button to play again
			GUI.backgroundColor = Color.clear;
			if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 10) * 6, Screen.width - 200, (int)(Screen.height / 10)), replay)) {
				//AdMobPlugin.HideBannerView();
				//Social.ShowLeaderboardUI();
				Application.LoadLevel ("main");
				//string urlString = "market://details?id=" + "com.BenPollock.RedSeaRescue";
			//	Application.OpenURL(urlString);

			}
			if (GUI.Button (new Rect (100, Screen.height - (int)(Screen.height / 10) * 5, Screen.width - 200, (int)(Screen.height / 10)), quit)) {
				AdMobPlugin.HideBannerView();
				Application.LoadLevel ("splash");
			}

			if (GUI.Button (new Rect (25, Screen.height - (int)(Screen.height / 20) * 7, (int)(Screen.width /2) - 50, (int)(Screen.height / 10)), leaderboard)) {
				Debug.Log ("press leaderboard");
			}

			if (GUI.Button (new Rect ((int)(Screen.width/2)+25, Screen.height - (int)(Screen.height / 20) * 7, (int)(Screen.width /2) - 50, (int)(Screen.height / 10)), achievements)) {
				Debug.Log ("press leaderboard");
			}

			
		}
	}

}
