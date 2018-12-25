using UnityEngine;
using System.Collections;

public class SpawnDetect : MonoBehaviour {
	void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, new Vector3(1, 15, 0));
	}

}
