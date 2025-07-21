using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public GameObject[] spawners;
	int currentSlot;
	int totalCount;
	float delayTime;
	void Start () {
		currentSlot = Random.Range (0,20);
		totalCount = 0;
		for(int i=0;i<spawners.Length;i++){
			spawners [i].SetActive (false);
		}
		SpawnNew ();
		delayTime = 4.1f;
	}
	void Update () {
		
		
				if(totalCount>=19){
			gameObject.GetComponent<SpawnManager>().enabled=false;
				}
			}
	IEnumerator WaitForNextSpawn(){
				
		yield return new WaitForSecondsRealtime (delayTime);
				Debug.Log ("Total: "+totalCount);
				if(totalCount<20){
					SpawnNew ();
				}
		
		
			}
	void SpawnNew (){
		delayTime = delayTime - 0.10f;
		totalCount++;
		if(spawners[currentSlot]!=null){
			spawners [currentSlot].SetActive (true);
			spawners [currentSlot].GetComponent<Spawner> ().StartSpawning ();
		}
		currentSlot++;
		if(currentSlot>=19){
			currentSlot = 0;
		}
		StartCoroutine( WaitForNextSpawn ());
	}



}
