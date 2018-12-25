using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ItemCount : MonoBehaviour {

	protected Text tex;

	// Use this for initialization
	void Start () {
		tex = GetComponent<Text>();
		SetCountToText();
	}
	
	void Update () {
		SetCountToText();
	}

	protected abstract int GetItemCount();

	void SetCountToText() {
		int count = GetItemCount();
		tex.text = "" + count;
	}
}

