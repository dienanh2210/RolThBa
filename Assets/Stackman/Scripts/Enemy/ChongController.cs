using UnityEngine;
using System.Collections;

public class ChongController : MonoBehaviour {
	private bool attackedPlayer;
	private Animator animator;
	public GameObject particle;

	void Start () {
		attackedPlayer = false;
		if (transform.parent != null) animator = transform.parent.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (attackedPlayer) return;
		string tag = col.gameObject.tag;
		
		if (tag == "Player") {
			col.gameObject.GetComponent<PlayerController>().beAttacked(20);
			attackedPlayer = true;
		}

		if (tag == "SpawnGroundDetect") {
			Invoke("Appear", 2);
		}

		if (tag == "Chuong" || tag == "Dragon"
			|| tag == "_Chuong" || tag == "_Dragon"
			|| tag == "PlayerBigSlash" || tag == "PlayerBigKunai"
			|| tag == "_PlayerBigSlash" || tag == "_PlayerBigKunai") {
//			Destroy(gameObject);

			if (animator != null) {
//				animator.SetTrigger("Disappear");	
				if (particle != null) {
					Instantiate(particle, transform.position + new Vector3(-0.3f, 0.5f, 0), Quaternion.identity);//Euler(270, 0, 0));	
//					Debug.Log("den day roi");
				}
				Destroy(gameObject);
			}
			else {
				if (particle != null) {
					Instantiate(particle, transform.position + new Vector3(0, 0, 0), Quaternion.identity);//Euler(270, 0, 0));	
				}
				Destroy(gameObject);
			}
		} 
	}

	void Appear() {
		if (animator != null)
			animator.SetBool("Appear", true);
	}

}
