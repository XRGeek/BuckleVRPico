using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DigitalRuby.SoundManagerNamespace;
//using UnityEngine.Advertisements;
public class Timer : MonoBehaviour {
	public static float maxTimer;
	public float degradePerc = .02f;
	public TextMesh timerText;
	public Image timeEndPanel;
	public float currentMaxTime;
	private float currentTime;
	public bool isCountingDown;
	ScoreManager scoreManager;
	GameObject[] enemies;
	bool playingGame;
	public AudioClip gameOver;
	public GameObject gamePlayAudio;
	public GameObject BGAudio;
	public Image[] finalInstruction;
	public GamePlaySoundManager tracksound;

	int instructionNumber;
	void Awake()

	{	
		for(int i=0;i<finalInstruction.Length;i++){
			finalInstruction [i].gameObject.SetActive (false);
		}
		playingGame=true;
		Time.timeScale = 1;
		scoreManager = GetComponent<ScoreManager> ();
		timeEndPanel.gameObject.SetActive (false);
		StartTimer ();
	}

	void Update()
	{
		if (isCountingDown) {
			UpdateCounter ();
			DisplayTime ();
		}
	}

	public void StartTimer()
	{
		currentTime = currentMaxTime;
		isCountingDown = true;

	}

	public void ResetTimer()
	{
		currentMaxTime -= maxTimer * degradePerc;
		StartTimer();
	}

	void UpdateCounter()
	{
		currentTime -= Time.deltaTime;
		if (currentTime < 0)
		{
			GameOver ();

		}
		//DisplayTime();
	}

	void DisplayTime()
	{	
		if (playingGame) {
			int min;
			float sec;
			float displayTimer = Mathf.Round (currentTime);
			min = (int)(displayTimer / 60);
			sec = (int)(displayTimer % 60);
			if (sec < 10)
				timerText.text = min + ":0" + sec;
			else
				timerText.text = min + ":" + sec;

		}
	}
	IEnumerator waitForFinalPanel(){
		yield return new WaitForSecondsRealtime (2);


		StartCoroutine (FadeEffect.FadeOut(timeEndPanel,1));
		StartCoroutine (FadeEffect.FadeOut(scoreManager.scoreText,1));
		StartCoroutine (FadeEffect.FadeOut(scoreManager.scorePanel,1));
		StartCoroutine (finalPanel());
	}
	IEnumerator finalPanel(){
		yield return new WaitForSecondsRealtime (2);
		scoreManager.finalScorePanel.gameObject.SetActive (true);
		instructionNumber = PlayerPrefs.GetInt ("InstructionNumber");
		instructionNumber++;
		if(instructionNumber>=3){
			instructionNumber = 0;
		}
		PlayerPrefs.SetInt ("InstructionNumber",instructionNumber);
		finalInstruction [instructionNumber].gameObject.SetActive (true);
		StartCoroutine (FadeEffect.FadeIn(finalInstruction [instructionNumber],1));
		StartCoroutine (FadeEffect.FadeIn(scoreManager.finalScorePanel,1));
		StartCoroutine (FadeEffect.FadeIn(scoreManager.finalScoreText,1));
		scoreManager.ShowFinalScore ();
	}
	public void GameOver(){
		if(playingGame){
			timerText.gameObject.SetActive (false);
			playingGame = false;
			Destroy (gamePlayAudio.GetComponent<GamePlayAudio>());
			//Destroy (BGAudio);
			tracksound.StopMenuTrack();
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			for(int i=0 ; i < enemies.Length ; i++){
				if(enemies[i]!=null)
					StartCoroutine (FadeEffect.FadeOut(enemies[i].transform.GetChild(0).gameObject.GetComponent<Image>(),1.0f));
			}
			StartCoroutine (WaitForGameOver());

		}
	}
	IEnumerator WaitForGameOver(){
		yield return new WaitForSecondsRealtime (2);
		for(int i=0 ; i < enemies.Length ; i++){
			Destroy(enemies[i]);

		}
		timeEndPanel.gameObject.SetActive (true);

		StartCoroutine (waitForFinalPanel());
		StartCoroutine (FadeEffect.FadeIn(timeEndPanel,1));
		gameObject.GetComponent<AudioSource> ().PlayOneShot (gameOver);
		StartCoroutine (WiatForEndSound());
		currentTime = 0;
		isCountingDown = false;
	}
	IEnumerator WiatForEndSound(){
		yield return new WaitForSecondsRealtime (2);
		//gamePlayAudio.GetComponent<AudioSource> ().Play ();
		tracksound.PlayEndMusic();
	}


}