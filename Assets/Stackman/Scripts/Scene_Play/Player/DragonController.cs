using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour {

	public float speed = GameConst.DRAGON_SPEED;

	private Rigidbody2D rigid2D;

	void Start() {
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
	}
}
