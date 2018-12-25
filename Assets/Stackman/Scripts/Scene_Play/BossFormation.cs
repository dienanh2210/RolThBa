using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossFormation : MonoBehaviour {

	public GameObject[] bosses;
	public GameObject bossHPBG;
	public GameObject bossHPProgress;
	public GameObject[] bonuses;
	private int countBoss;

	private int bossHPBalance;

	public int BossHPBalance {
		get {
			return bossHPBalance;
		}
	}

	public GameObject BossHPProgress {
		get {
			return bossHPProgress;
		}
		set {
			bossHPProgress = value;
		}
	}

	public int pos;

	// Use this for initialization
	void Start () {
		pos = ItemData.Pos;
		bossHPBalance = 150;
		countBoss = 0;
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 1);
	}

	public void BossAppear() {
		countBoss++;

		bossHPBalance += 65;
		GameObject bossObj = Instantiate(bosses[pos], new Vector3(3.49f, -2.05f, 1), Quaternion.identity) as GameObject;
		bossObj.transform.parent = transform.parent;

		Invoke("BossHPAppear", 1);
	}

	public void BossHPAppear() {
		bossHPBG.SetActive(true);
		bossHPProgress.SetActive(true);
	}

	public void BossHPDisappear() {
		bossHPBG.SetActive(false);
		bossHPProgress.SetActive(false);
	}

	public void ToVictory() {
		Invoke("Victory", 1);
	}

	void Victory() {
		SceneManager.LoadScene("Victory");
	}

	public void Bonus1Appear() {
		if (countBoss == 1) {
			Instantiate(bonuses[0], new Vector3(transform.position.x - 10, -5.85f, 0), Quaternion.identity);
		} else {
			int r = Random.Range(1, bonuses.Length);
			Instantiate(bonuses[r], new Vector3(transform.position.x - 10, -5.85f, 0), Quaternion.identity);	
		}

	}
}
