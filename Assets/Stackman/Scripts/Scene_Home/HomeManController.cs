using UnityEngine;
using System.Collections;

public class HomeManController : MonoBehaviour {

	public float speedX;
	public float speedY = -1.3f;
	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigid2D.velocity = new Vector3(speedX, speedY, 0);
	}
}
