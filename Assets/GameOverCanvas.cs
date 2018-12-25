using UnityEngine;
using System.Collections;

public class GameOverCanvas : MonoBehaviour {

	void OnEnable() {
		SoundManager.instance.PlaySingleByNameLoop(GameConst.WAIT_AUDIO);
	}

	void OnDisable() {
		SoundManager.instance.StopSingleLoop();
	}
}
