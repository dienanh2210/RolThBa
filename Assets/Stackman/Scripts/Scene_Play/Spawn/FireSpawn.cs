using UnityEngine;
using System.Collections;

public class FireSpawn : MonoBehaviour {
	public GameObject[] firePrefabs;

	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 0));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == GameConst.SPAWN_BALANCE_TAG) {
			int r = Random.Range(0, firePrefabs.Length);
			Instantiate(firePrefabs[r], transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

}
