using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {

	private Text tex;

	// Use this for initialization
	void Start () {
		tex = GetComponent<Text>();
		tex.text = DataPref.getNumData(GameConst.NUM_COLLECT_KEY).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			tex.text = DataPref.getNumData(GameConst.NUM_COLLECT_KEY).ToString();
		}
	}
}
