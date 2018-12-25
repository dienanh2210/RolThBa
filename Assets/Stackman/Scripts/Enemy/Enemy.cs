using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	
	public bool isDead;
	public GameObject[] fires;
	protected float speed;
	protected bool attackedPlayer = false;
	protected int HP;

	protected abstract void InitSpeed();
	protected abstract void InitHP();

	private GameObject temp;
	protected GameBalance gameBalance;
	protected GameObject player;
	protected PlayerController playerController;
	protected Animator animator;
	protected Collider2D col2D;
	protected Rigidbody2D rigid2D;
	private bool bonus;

	protected virtual void Start () {
		isDead = false;
		attackedPlayer = false;
		gameBalance = GameObject.Find("GameBalance").GetComponent<GameBalance>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
		col2D = GetComponent<Collider2D>();
		rigid2D = GetComponent<Rigidbody2D>();
		bonus = false;

		InitSpeed();
		InitHP();
	}

	protected virtual void Update () {
		if (isDead) {
			gameBalance.EnemyDieTrigger(gameObject.tag);

			Destroy(gameObject);
		}
		
		if (isAttackingPlayer() && attackedPlayer == false && animator.GetBool("Die") == false) {
			playerController.beAttacked(20);
			attackedPlayer = true;
		}
		Move();
	}

	protected abstract void Move();

	protected bool isAttackingPlayer() {
		Bounds enemyBound = col2D.bounds;
		Bounds playerBound = player.GetComponent<Collider2D>().bounds;
		return enemyBound.Intersects(playerBound);
	}

	protected virtual void OnTriggerEnter2D(Collider2D col) {
		if (animator.GetBool("Die")) return;
		string tag = col.gameObject.tag;

		// _tagname: tag after trigger, to avoid trigger with an obj twice;
		if (tag == "Dragon" || tag == "Chuong" || tag == "PlayerBigKunai" || tag == "PlayerBigSlash" || 
			tag == "_PlayerBigKunai" || tag == "_PlayerBigSlash" ||  tag == "_Dragon" || tag == "_Chuong") {
			HP -= GetPlayerPower(tag);
			if (tag[0] != '_' && gameObject.tag == "Boss1") col.gameObject.tag = "_" + tag;
		} else
			
		if (tag == "PlayerSlash" || tag == "PlayerSpecial") {
			HP -= GetPlayerPower(tag);
		} else
		if (tag == "PlayerKunai") {
			Destroy(col.gameObject);
			HP -= GetPlayerPower(tag);
		}

		if (HP <= 0) {
			animator.SetBool("Die", true);
			if (!bonus) {
				bonus = true;
				Bonus();
			}
			if (rigid2D != null) rigid2D.gravityScale = 1;
		}

	}

	protected abstract void Bonus();

	private int GetPlayerPower(string tag) {
		if (tag == "PlayerSlash") return GameConst.PLAYER_SLASH_POWER;
		if (tag == "Dragon" || tag == "_Dragon") return GameConst.PLAYER_DRAGON_POWER;
		if (tag == "Chuong" || tag == "_Chuong") return GameConst.PLAYER_CHUONG_POWER;
		if (tag == "PlayerKunai") return GameConst.PLAYER_KUNAI_POWER;
		if (tag == "PlayerBigKunai" || tag == "_PlayerBigKunai") return GameConst.PLAYER_BIG_KUNAI_POWER;
		if (tag == "PlayerBigSlash" || tag == "_PlayerBigSlash") return GameConst.PLAYER_BIG_KUNAI_POWER;
		if (tag == "PlayerSpecial") return GameConst.PLAYER_SPECIAL_POWER;
		return 10;
	}

	public bool IsDead {
		get {
			return isDead;
		}
		set {
			isDead = value;
		}
	}
}
