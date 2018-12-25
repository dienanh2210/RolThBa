using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Boss1Controller : Enemy {

	public GameObject[] chuongs;
	private int fullHP;
	private BossFormation bossFormation;
	private BossHPProgress bossHPProgress;
	public Animator animator2;

	protected override void InitHP () {
		this.HP = bossFormation.BossHPBalance;
//		Debug.Log(HP);
		fullHP = HP;
	}

	protected override void InitSpeed() {
	}
	
	protected override void Move() {
	}

	protected override void Start() {
		bossFormation = GameObject.Find("BossFormation").GetComponent<BossFormation>();
		bossHPProgress = bossFormation.BossHPProgress.GetComponent<BossHPProgress>();

		base.Start();

		UseChuong();
	}

	protected override void Update() {
		if (isDead) {
			bossFormation.BossHPDisappear();
			//TODO- 
			if (DistanceController.RunDistance > 50000) {
				Invoke("Victory", 1);
				return;
			}

			/* trigger su kien xuat hien boss tiep theo */
			// GameBalance -> appearNextBoss();
		}

		base.Update();

		if (bossHPProgress != null) bossHPProgress.SetProgress(Mathf.Max((int)((HP*1.0f/fullHP)*100), 0));
	}

	public void Victory() {
		GameStateManager gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
		gameStateManager.GoToVictory();
	}

	public void UseChuong() {
		animator.SetTrigger("Chuong");
		if (animator2 != null) animator2.SetTrigger("phun");

		Vector3 offset = new Vector3(2, 2.2f, 0);
		Instantiate(chuongs[Random.Range(0, chuongs.Length)], transform.position + offset, Quaternion.identity);
		Invoke("UseChuong", 2.3f);
	}

	protected override void OnTriggerEnter2D(Collider2D col) {
		string tag = col.gameObject.tag;
		if (tag == "_PlayerBigKunai" || tag == "_PlayerBigSlash"
			|| tag == "_Dragon" || tag == "_Chuong") {
			return;
		}
		base.OnTriggerEnter2D(col);
	}

	#region implemented abstract members of Enemy
	protected override void Bonus () {
		//TODO
		bossFormation.Bonus1Appear();
	}
	#endregion
}
