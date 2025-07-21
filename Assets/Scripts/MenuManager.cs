using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using DigitalRuby.SoundManagerNamespace;
public class MenuManager : MonoBehaviour {
	[SerializeField]
	GameObject startMenu;
	[SerializeField]
	Image startMenuImage;
	[SerializeField]
	GameObject directionMenu;
	[SerializeField]
	Image directionMenuImage;
	[Header("Enter Fade Speed For Menu Transition")]
	[SerializeField]
	float fadeSpeed;

	[Header("Drag and Drop Loading Screen")]
	[SerializeField]
	GameObject  loading;

	[Header("Drag and Drop Audio SFX")]
	[SerializeField] AudioClip click;
	[SerializeField] AudioClip hereWeGoClick;
	[SerializeField] AudioClip beginClick;

	[Header("Menu Track Control")]
	[SerializeField]
	MenuSoundManager BGTrack;

	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		//loading.SetActive (false);
		ScoreManager.score = 0;
		audioSource = gameObject.GetComponent<AudioSource> ();
		//audioSource.Play ();
		startMenu.SetActive (true);
		//directionMenu.SetActive (true);
		startMenuImage = startMenu.GetComponent<Image> ();
		directionMenuImage = directionMenu.GetComponent<Image> ();
	}


	public static MenuManager instance;

	void Awake () {
		Debug.Log ("Awake");
		AudioConfiguration config = AudioSettings.GetConfiguration();
		config.dspBufferSize = 2048; // Best Performance is 1024 on Samsung S6 in the GearVR
		AudioSettings.Reset(config);
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 60;

		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}
	void Update(){
		
	}

	public void StartGame(){
		Destroy (startMenu.transform.GetChild(0).gameObject);
		directionMenu.SetActive (true);
		audioSource.PlayOneShot (beginClick);
		directionMenu.transform.GetChild (0).gameObject.SetActive (false);
		StartCoroutine( FadeEffect.FadeOut (startMenuImage,fadeSpeed));
		StartCoroutine (WaitForDirection());
	}
	public void Begin(){
		Destroy (directionMenu.transform.GetChild(0).gameObject);
		audioSource.PlayOneShot (hereWeGoClick);
		StartCoroutine( FadeEffect.FadeOut (directionMenuImage,fadeSpeed));
		//StartCoroutine(LoadYourAsyncScene());
		StartCoroutine (WaitForGamePlay());
	}
	IEnumerator WaitForDirection(){
		yield return new WaitForSecondsRealtime (1);
		StartCoroutine( FadeEffect.FadeIn (directionMenuImage,fadeSpeed));
		directionMenu.transform.GetChild (0).gameObject.SetActive (true);
	}
	IEnumerator WaitForGamePlay(){
		yield return new WaitForSecondsRealtime (1);
		directionMenu.SetActive (false);
		//loading.SetActive (true);
		BGTrack.StopMenuTrack();
		SceneCreator.instance.SetActiveMenu(false);
		SceneCreator.instance.roof.SetActive(false);
		SceneCreator.instance.SetActiveGamePlay(true);
			
		//SceneManager.LoadScene (1);
	}
	IEnumerator LoadYourAsyncScene()
	{
		// The Application loads the Scene in the background at the same time as the current Scene.
		//This is particularly good for creating loading screens. You could also load the Scene by build //number.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePlay");

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
