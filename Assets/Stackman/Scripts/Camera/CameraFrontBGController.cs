using UnityEngine;
using System.Collections;

public class CameraFrontBGController : MonoBehaviour {
	public float scrollSpeed;
	private float tileSizeZ;
	public GameObject bg1;
	public GameObject bg2;
	public GameObject mainCam;

	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
		tileSizeZ = bg2.transform.position.x - bg1.transform.position.x;
	}

	void Update (){
		if (Mathf.Abs(scrollSpeed) < float.Epsilon) return;

		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.right * newPosition;

		transform.position = new Vector3(transform.position.x, mainCam.transform.position.y + 27.46748f, transform.position.z);
	}

	public float ScrollSpeed {
		get {
			return scrollSpeed;
		}
		set {
			scrollSpeed = value;
		}
	}
}
