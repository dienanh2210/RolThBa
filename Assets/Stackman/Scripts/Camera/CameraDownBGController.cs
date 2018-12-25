using UnityEngine;
using System.Collections;

public class CameraDownBGController : MonoBehaviour {

	private Vector3 startPosition;
	public PlayerController player;

	void Start () {
		startPosition = transform.position;
	}
	
	void Update () {
		float offsetY = player.GetOffsetWithCurrent().y;
		if (offsetY < 0) offsetY = 0; //Truong hop player bi roi xuong vuc

		transform.position = startPosition + new Vector3(0, offsetY / 15, 0);
	}
}
