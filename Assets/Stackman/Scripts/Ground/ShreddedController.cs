using UnityEngine;
using System.Collections;

public class ShreddedController : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void Update () {
		FollowPlayer();
	}

	void FollowPlayer() {
		GetComponent<Rigidbody2D>().velocity = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			player.GetComponent<PlayerController>().beAttacked(100);
		} else
			if (col.gameObject.tag != "Shredded" && col.gameObject.tag != "PlayerSlash")
			Destroy(col.gameObject);
	}

}
