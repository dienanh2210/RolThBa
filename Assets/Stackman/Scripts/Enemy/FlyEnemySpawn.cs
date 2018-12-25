using UnityEngine;
using System.Collections;

public class FlyEnemySpawn : MonoBehaviour {

	public GameObject[] flyEnemies;
	private int pos;

	// Use this for initialization
	void Start () {
		pos = ItemData.Pos;
	}

	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 0));
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == GameConst.SPAWN_BALANCE_TAG) {
			Instantiate(flyEnemies[pos], transform.position, Quaternion.identity);
		}
	}

}
