using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class CameraController : MonoBehaviour {

	public PlayerController player;
	public DistanceController distanceController;
	public GameBalance gameBalance;

	private Vector3 startPosition;
	private float positionY;
	private float oldPositionY;
	private bool canSetPositionY;

	private float DIS_PLAYER_CAM = 3.5f;

	public int followSpecial = 0;

	void Awake() {
		SetPosForScene();
	}

	void Start() {
		startPosition = this.transform.position;
		canSetPositionY = true;
		followSpecial = 0;
	}

	public void SetPositionY(float y) {
		if (!canSetPositionY) return;
		if (Mathf.Abs(y - positionY) < float.Epsilon) return;

		oldPositionY = positionY;
		positionY = y;
		canSetPositionY = false;
		Invoke("EnableCanSetPositionY", 2);
	}

	void EnableCanSetPositionY() {
		canSetPositionY = true;
	}


	void Update () {
		if (followSpecial == 1) FollowSpecialPlayer();
		else if (followSpecial == 2) FinishSpecial();
		else 
			FollowPlayer();

		float distance = this.transform.position.x - startPosition.x;
		updateDistance(distance);
	}

	void FollowPlayer() {
		if (player.isDead) return;

		float offsetY = player.GetOffsetWithCurrent().y;

		if (offsetY < 0) offsetY = 0; //Truong hop player bi roi xuong vuc
		if (Mathf.Abs(positionY - oldPositionY) > 0.1) {
			//1
			if (oldPositionY < positionY) oldPositionY += Time.deltaTime;
			else oldPositionY -= Time.deltaTime;

			transform.position = new Vector3(player.transform.position.x + DIS_PLAYER_CAM, oldPositionY + offsetY / 3.5f, transform.position.z);
		} else {
			//2
			transform.position = new Vector3(player.transform.position.x + DIS_PLAYER_CAM, positionY + offsetY / 3.5f, transform.position.z);
		}
	}

	void FollowSpecialPlayer() {
		if (transform.position.x - player.transform.position.x > 0) 
			transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y, transform.position.z);
		else {
			transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
			player.UpDownSpecial();
		}
			
	}

	void FinishSpecial() {
		if (transform.position.x - player.transform.position.x < DIS_PLAYER_CAM) 
			transform.position = new Vector3(transform.position.x + Time.deltaTime*20, transform.position.y, transform.position.z);
		else 
		{
			transform.position = new Vector3(player.transform.position.x + DIS_PLAYER_CAM, transform.position.y, transform.position.z);
			followSpecial = 0;
			player.afterFinishSpecialSkill();
		}
	}

	public void setSpecial(int special) {
		followSpecial = special;
	}

	void updateDistance(float dis) {
		dis = dis * 6;
		distanceController.updateDistance(dis);
		gameBalance.SetDistance(dis);
	}

	public void updatePosition(float offset) {
	}

	public void SetPosForScene() {
		string name = SceneManager.GetActiveScene().name;
		if (name == "Play0") ItemData.Pos = 0;
		else if (name == "Play1") ItemData.Pos = 1;
		else if (name == "Play2") ItemData.Pos = 2;
		else if (name == "Play3") ItemData.Pos = 3;
		else if (name == "Play4") ItemData.Pos = 4;
	}
}
