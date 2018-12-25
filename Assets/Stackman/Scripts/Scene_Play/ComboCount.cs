using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboCount : MonoBehaviour {

	public static int comboNum;
	private Text tex;

	public static int ComboNum {
		get {
			return comboNum;
		}
		set {
			comboNum = value;
		}
	}

	// Use this for initialization
	void Start () {
		tex = GetComponent<Text>();
		tex.text = "" + comboNum;
	}

	void Update() {
		tex.text = "" + comboNum;
	}
	
}
