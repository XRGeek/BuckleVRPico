using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HoverEffect : MonoBehaviour {

	public static void OnPointerEnter(Image image){
		image.color = new Color(200,200,200);
	}
	public static void OnPointerExit(Image image){
		image.color = Color.white;
	}
}
