using UnityEngine;
using System.Collections;

public class BossChuong1 : MonoBehaviour {

	private float speed = GameConst.BOSS_CHUONG_SPEED;
	private bool isAttacked;
	private Rigidbody2D rigid2D;
	public GameObject particle;
//	private PlayerController player;

	// Use this for initialization
	void Start () {
		isAttacked = false;
		rigid2D = GetComponent<Rigidbody2D>();
//		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	void FixedUpdate () {
		rigid2D.velocity = new Vector2(- speed, rigid2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other) {

		string tag = other.gameObject.tag;

		if (other.gameObject.tag == "Player") {
			if (!isAttacked) {
				other.gameObject.GetComponent<PlayerController>().beAttacked(20);
				isAttacked = true;
			}
		}

		if (tag == "Chuong" || tag == "Dragon"
			|| tag == "_Chuong" || tag == "_Dragon"
			|| tag == "PlayerBigSlash" || tag == "PlayerBigKunai"
			|| tag == "_PlayerBigSlash" || tag == "_PlayerBigKunai"
			|| tag == "PlayerSlash" || tag == "PlayerKunai") {
			Instantiate(particle, transform.position, Quaternion.Euler(270, 0, 0));
				Destroy(gameObject);

		}
	}
}
