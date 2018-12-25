using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopTextController : MonoBehaviour {

	private Text tex;

	void Start () {
		tex = GetComponent<Text>();
	}
	
	void Update () {
		tex.text = DataPref.getNumData(GameConst.NUM_COLLECT_KEY).ToString();
	}
}
