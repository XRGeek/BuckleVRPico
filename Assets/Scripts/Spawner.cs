using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour {
	public static bool isClickAudio;
	public Image startLine;
	public Image spawnObject;
	GameObject player;
	public float Delay = 5.0f;
	public float distance;
	public static bool isPlaying;
	GameObject[] enemies;
	public bool canBuckle;
	public int passengerType;
	public Sprite oppsImage;
	public Sprite[] passenger;
	public Sprite[] buckledPassenger;
	public GameObject click1;
	public GameObject click2;
	public GameObject click3;
	public GameObject laser;
	[Range(10f,50f)]
	public float distanceToDie=50f;
	AudioSource audioSource;
	public AudioClip opps;
	public AudioClip buckleClick;
	public AudioClip rocketEffect;
	public AudioClip gameOverEffect;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		StartSpawning ();
	}
	public void StartSpawning(){
		int maxValue;

		maxValue = passenger.Length;
		passengerType = Random.Range (0,maxValue);
		spawnObject.sprite=passenger[passengerType];
//		Debug.Log (passengerType);
		canBuckle = false;
		isPlaying = true;
		player = GameObject.FindGameObjectWithTag ("MainCamera");
		StartCoroutine( FadeEffect.FadeIn (startLine, 1.0f));
		StartCoroutine(WaitForComeUp());

	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying){
			distance=Vector3.Distance (spawnObject.gameObject.transform.position,player.transform.position);
			//Debug.Log (distance+" Type "+ passengerType);
			if(distance<distanceToDie){
				//GameObject.FindGameObjectWithTag ("Manager").GetComponent<Timer>().GameOver();
				//isPlaying=false;
				//ScoreManager.score--;
				Destroy(gameObject);
				//Debug.Log (distance);
			}

		}
		if(Input.GetKeyDown(KeyCode.C) && canBuckle){
			Buckled ();
		}
	}
	IEnumerator WaitForComeUp(){
		yield return new WaitForSecondsRealtime (2);
		iTween.MoveBy(spawnObject.gameObject,iTween.Hash(
			"y"   , 31.48f,
			"time", 3.2f
		));
		StartCoroutine (WaitToMove());
		//StartCoroutine (waitForCanBuckle());
	}
//	IEnumerator waitForCanBuckle(){
//		yield return new WaitForSecondsRealtime (1.5f);
//		canBuckle = true;
//	}
	IEnumerator WaitToMove(){
		yield return new WaitForSecondsRealtime (3.0f);
		Destroy (startLine);
		canBuckle = true;
		iTween.MoveTo (spawnObject.gameObject, player.transform.position, Delay);
	}
	public void Buckled(){
		

		Destroy (click1);
		Destroy (click2);
		Destroy (click3);
		if (passengerType == 0) {
			gameObject.transform.GetChild (0).gameObject.transform.GetChild (1).gameObject.GetComponent<Image> ().sprite = oppsImage;
			//ScoreManager.score--;
			audioSource.PlayOneShot (opps);

		} else {
			
			if (isClickAudio) {
				isClickAudio = false;
				audioSource.PlayOneShot (rocketEffect);
			} else {
				isClickAudio = true;
				audioSource.PlayOneShot (buckleClick);
			}

			//audioSource.PlayOneShot (rocketEffect);
			ScoreManager.score++;
		}
		spawnObject.sprite=buckledPassenger[passengerType];

		iTween.Stop (spawnObject.gameObject);
		gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.SetActive (true);
		gameObject.transform.GetChild (0).gameObject.transform.GetChild (1).gameObject.SetActive (true);
		StartCoroutine (WaitForMoveUp());


	}
	IEnumerator WaitForMoveUp(){
		yield return new WaitForSeconds (0.1f); 
		iTween.MoveBy(spawnObject.gameObject,iTween.Hash(
			"y"   , 300f,
			"time", 5.2f
		));
		Destroy (spawnObject.gameObject.transform.GetChild (2).GetComponent<BoxCollider> ());
		laser.GetComponent<MenuRay> ().MakeLaserNoCharacterSelected();
		StartCoroutine (WiatForFinalDestroy());
	}
	IEnumerator WiatForFinalDestroy(){
		yield return new WaitForSeconds (4.1f); 
		Destroy (gameObject);
	}
	public void UnSelectAll(){
		if (click1 != null) {
			click1.GetComponent<Image> ().enabled = false;
		}
		if (click2 != null) {
			click2.GetComponent<Image> ().enabled = false;
		}
		if (click1 != null) {
			click2.GetComponent<Image> ().enabled = false;
		}
	}
	public void EnableClick1(){
		if(canBuckle){
			click1.GetComponent<Image> ().enabled = true;
		}
	}
	public bool CheackClcik1(){
		return click1.GetComponent<Image> ().isActiveAndEnabled;
	}
	public void EnableClick2(){
		if(canBuckle){
			click2.GetComponent<Image> ().enabled = true;
		}
	}
	public void EnableClick3(){
		if (canBuckle) {
			click3.GetComponent<Image> ().enabled = true;
			Buckled ();
		}
	}
	public bool CheackClcik2(){
		return click1.GetComponent<Image> ().isActiveAndEnabled;
	}

}
