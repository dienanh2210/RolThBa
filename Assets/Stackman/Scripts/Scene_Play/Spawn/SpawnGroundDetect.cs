using UnityEngine;
using System.Collections;

public class SpawnGroundDetect : MonoBehaviour {

	private Rigidbody2D playerRigid2D;
	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		playerRigid2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigid2D.velocity = new Vector3(playerRigid2D.velocity.x, 0, 0);
	}

}
