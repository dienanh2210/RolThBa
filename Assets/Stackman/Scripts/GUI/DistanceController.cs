using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour {
	public static int runDistance;

	public static int RunDistance {
		get {
			return runDistance;
		}
		set {
			runDistance = value;
		}
	}

	private Text tex;

	void Start() {
		tex = GetComponent<Text>();
	}

	public void updateDistance(float dis) {
		tex.text = (int)dis + "";
		runDistance = (int) dis;
	}
}
