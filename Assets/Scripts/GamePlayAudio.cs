using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayAudio : MonoBehaviour {
	public AudioClip[] clip;
	AudioSource audioSource;
	int count;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		count = 0;
		StartCoroutine (PlaySound());
	}
	
	IEnumerator PlaySound(){
		yield return new WaitForSecondsRealtime (16.5f);
		audioSource.PlayOneShot (clip[count]);
		count++;
		if(count >=3){
			count = 0;
		}
		StartCoroutine (PlaySound());
	}
}
