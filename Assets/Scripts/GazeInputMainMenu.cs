using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GazeInputMainMenu : MonoBehaviour {
	private Ray ray;
	private RaycastHit hitInfo;
	public Image loadingCircle;
	float currentFillAmount;
	[SerializeField]
	MenuManager menu;
	// Use this for initialization
	void Start () {
		loadingCircle.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit ();
		}
		ray = new Ray (transform.position,gameObject.transform.forward);
		Debug.DrawRay (transform.position,gameObject.transform.forward*100,Color.yellow);
		if(Physics.Raycast(ray,out hitInfo)){
			
			if (hitInfo.collider.tag == "Start") {
				//Debug.Log ("Acute Cude Detected");
				currentFillAmount=currentFillAmount+Time.deltaTime;
				loadingCircle.fillAmount = currentFillAmount;
				if(loadingCircle.fillAmount>=0.99f){
					menu.StartGame ();
					currentFillAmount = 0;
					loadingCircle.fillAmount = 0;
				}
			}
			if (hitInfo.collider.tag == "Begin") {
				//Debug.Log ("Acute Cude Detected");
				currentFillAmount=currentFillAmount+Time.deltaTime;
				loadingCircle.fillAmount = currentFillAmount;
				if(loadingCircle.fillAmount>=0.99f){
					menu.Begin ();
				}
			}

		} else {
			currentFillAmount = 0;
			loadingCircle.fillAmount = 0;
		}
	}
}
