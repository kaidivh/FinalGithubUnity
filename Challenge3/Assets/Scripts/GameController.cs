using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float waveWait;
	public float spawnWait;
	public float startWait;
	
	public bool hardMode;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	
	public AudioSource audioSource;
	public AudioClip backgroundMusic;
	public AudioClip defeatMusic;
	public AudioClip victoryMusic;
	
	public int score = 0;//made public so other game objects can access with less hassle
	
	private bool gameOver;
	private bool restart;
	
	
	
	void Start()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		UpdateScore();
		audioSource.clip = backgroundMusic;
		audioSource.Play();
		StartCoroutine( SpawnWaves());
	}
	
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		
		if (restart){
			if (Input.GetKeyDown(KeyCode.Y)){
				SceneManager.LoadScene("SpaceShooter");
			}
		}
		
		if (Input.GetKey("h") && hardMode == false){
			 hardMode = true;
		}
		
		
	}
	
	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++){
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'Y' for Restart!";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Points: " + score;
		if (score >= 500){
			GameOver();
		}
	}
	
	public void GameOver(){
		if (score < 500){
			audioSource.Stop();
			audioSource.clip = defeatMusic;
			audioSource.Play();
			gameOverText.text = "Game Over! :-( Game by Collin Conner!";
		}
		else{
			if (audioSource.clip != victoryMusic){ // otherwise each additional point gain restarts music
				audioSource.Stop();
				audioSource.clip = victoryMusic;
				audioSource.Play();
			}
			gameOverText.text = "You win! Game by Collin Conner!";
		}
		gameOver = true;
	}
}
