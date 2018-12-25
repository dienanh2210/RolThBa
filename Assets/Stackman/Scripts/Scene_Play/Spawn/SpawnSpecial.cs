using UnityEngine;
using System.Collections;

public class SpawnSpecial : MonoBehaviour {

	public GameObject specialPrefab;

	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 0));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == GameConst.SPAWN_BALANCE_TAG) {
			Instantiate(specialPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
