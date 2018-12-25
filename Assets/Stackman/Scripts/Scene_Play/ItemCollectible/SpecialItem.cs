using UnityEngine;
using System.Collections;

public class SpecialItem : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;
		if (tag == "Player") {
			SoundManager.instance.PlaySingleByName(GameConst.PICKUP_AUDIO);
			other.GetComponent<PlayerController>().UseSpecialSkill();
			Destroy(gameObject);
		}
	}
}
