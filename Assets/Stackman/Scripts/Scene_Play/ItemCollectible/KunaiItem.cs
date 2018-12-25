using UnityEngine;
using System.Collections;

public class KunaiItem : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;
		if (tag == "Player" || tag == "PlayerSpecial") {
			ItemData.AddSuriken(30);
			SoundManager.instance.PlaySingleByName(GameConst.PICKUP_AUDIO);
			Destroy(gameObject);
		}
	}
}
