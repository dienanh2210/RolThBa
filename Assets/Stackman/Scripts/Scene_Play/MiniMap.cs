using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {
	private float startX;
	private float length = 325;
	private RectTransform rectTransform;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
		startX = rectTransform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		float posX = startX + length*DistanceController.runDistance*1.0f / 50000;

		rectTransform.localPosition = new Vector3(posX, 
			rectTransform.localPosition.y,
			rectTransform.localPosition.z);

	}
}
