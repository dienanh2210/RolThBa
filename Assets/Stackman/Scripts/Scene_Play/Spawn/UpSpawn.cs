using UnityEngine;
using System.Collections;

public class UpSpawn : MonoBehaviour {

	public GameObject upPrefab;

	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 0));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == GameConst.SPAWN_BALANCE_TAG) {
			Instantiate(upPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
