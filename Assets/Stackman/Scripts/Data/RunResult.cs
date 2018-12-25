using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunResult : MonoBehaviour {
		
	int count, maxCount, unit;
	private Text tex;

	// Use this for initialization
	void Start () {
		count = 0;
		tex = GetComponent<Text>();
		tex.text = count + " M";
		maxCount = DistanceController.runDistance;
		unit = maxCount / GameConst.TIME_INC_ANIM;
	}
	
	// Update is called once per frame
	void Update () {
		if (count >= maxCount) return;

		count += unit;
		if (count >= maxCount) {
			count = maxCount;
		}

		tex.text = count + " M";

	}
}
