using UnityEngine;
using System.Collections;

public class GroundFormation : MonoBehaviour {
	public GameObject groundController;
	
	void SpawnGround() {
		GameObject groundControllerObject = Instantiate(groundController, transform.position, Quaternion.identity) as GameObject;
		GameObject parent = GameObject.FindGameObjectWithTag("Platform");
		groundControllerObject.transform.parent = parent.transform;
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 1);
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.tag == "SpawnGroundDetect") 
			SpawnGround();
	}

}
