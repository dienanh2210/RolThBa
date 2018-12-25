using UnityEngine;
using System.Collections;

public class KunaiSpawn : MonoBehaviour {
	public GameObject kunaiItemPrefab;

	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 0));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == GameConst.SPAWN_BALANCE_TAG) {
			Instantiate(kunaiItemPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
