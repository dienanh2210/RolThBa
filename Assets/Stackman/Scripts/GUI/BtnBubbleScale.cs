using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BtnBubbleScale : MonoBehaviour {

	public void OnTouchDown() {
		GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x*0.8f, 
		                                                       GetComponent<RectTransform>().localScale.y*0.8f, 
		                                                       GetComponent<RectTransform>().localScale.z);
	}
	public void OnTouchUp() {
		GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x/0.8f, 
		                                                       GetComponent<RectTransform>().localScale.y/0.8f, 
		                                                       GetComponent<RectTransform>().localScale.z);
	}

}
