using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingAnimator : MonoBehaviour {
	public Sprite[] img;
	int currentImage;
	// Use this for initialization
	void Start () {
		currentImage = 0;
		StartCoroutine (AnimateLoading());
	}
	IEnumerator AnimateLoading(){
		yield return new WaitForSecondsRealtime (0.6f);
		currentImage++;
		if(currentImage >=3){
			currentImage = 0;
		}
		gameObject.GetComponent<Image> ().sprite = img [currentImage];
		StartCoroutine (AnimateLoading());
	}
	

}
