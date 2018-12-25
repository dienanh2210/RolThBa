using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumSaveMeController : MonoBehaviour {

	private Text tex;

	// Use this for initialization
	void Start () {
		tex = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		tex.text = "" + ItemData.NumToSave;
	}
}
