using UnityEngine;
using System.Collections;

public class UpItem : MonoBehaviour {

	public GameObject effect;

	void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;
		if (tag == "Player") {
			SoundManager.instance.PlaySingleByName(GameConst.PICKUP_AUDIO);
			ItemData.AddUp(1);

			GameObject ef = Instantiate(effect, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity) as GameObject;
			GameObject cam = GameObject.Find("Main Camera");
			ef.transform.SetParent(cam.transform);

			Destroy(gameObject);
		}
	}
}
