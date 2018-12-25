using UnityEngine;
using System.Collections;

public class ChuongController : MonoBehaviour {

	public bool isOut;
	private GameObject player;

	void Start () {
		isOut = false;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		Vector3 offset = new Vector3(GameConst.CHUONG_OFFSET_X, GameConst.CHUONG_OFFSET_Y, 0);
		this.transform.position = player.transform.position + offset;

		if (isOut) Destroy(gameObject);
	}
}
