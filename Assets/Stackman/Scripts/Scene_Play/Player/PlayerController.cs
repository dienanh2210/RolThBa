using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class PlayerController : MonoBehaviour {

	public GameObject slash;
	public GameObject superSlash;
	public GameObject kunai;
	public GameObject dragonSkill;
	public GameObject chuongSkill;
	public GameObject bigKunai;
	public GameObject bigSlash;
	public GameObject circleEffectPrefab;
	public GameObject specialObj;
	public GameObject gameOverCanvas;
	public CameraController camController;
	public HPController hpController;
	public CameraUpBGController camUp;
	public CameraFrontBGController camFront;
	public GameObject magnetObj;
	public GameObject rebornEffect;
	public GameObject BaKunai;

	public Transform groundCheck;
	public Transform rightCheck;
	public LayerMask whatIsGround;

	private float speed = GameConst.PLAYER_SPEED;
	private float jumpHeight = GameConst.PLAYER_JUMP_HEIGHT;
	private float kunaiSpeed = GameConst.KUNAI_SPEED;
	private bool grounded = false;
	private float groundRadius = 0.2f;
	private bool doubleJump = false;

	private Vector3 currentPosition;
	private float positionYBeforeUpDown;

	private bool ButtonHolding;
	private float timeButtonHolding;

	private GameObject circleEffect;
	private bool special;
	private bool upDown = false;
	private int signUpDown = 1;

	public bool finishedChem;
	public bool finishedSuriken;

	public bool isDead;
	public bool isChem1;
	public bool isChem2;
	public bool isChem3;

	bool isGameOver;

	float timeGoToGOV;

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigid2D;
	private Collider2D col2D;

	public bool saved;

	private bool cantChuong, cantDragon;
	private bool battu;

	// Use this for initialization
	void Start () {

		//TODO
//		DataPref.setNumData(GameConst.NUM_COLLECT_KEY, 50000);

		timeButtonHolding = 0;
		timeGoToGOV = 0;
		ButtonHolding = false;
		circleEffect = Instantiate(circleEffectPrefab, transform.position + new Vector3(-0.5f, 1.2f, -1), Quaternion.identity) as GameObject;
		circleEffect.transform.parent = this.transform;
		circleEffect.SetActive(false);
//		Debug.Log (GetComponent<Rigidbody2D>().gravityScale);
		special = false;
		upDown = false;
		battu = false;

		specialObj.SetActive(false);

		isDead = false;
		isGameOver = false;
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigid2D = GetComponent<Rigidbody2D>();
		col2D = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) return;
		if (ButtonHolding) timeButtonHolding += Time.deltaTime;
		if (timeButtonHolding > 0.3f) {
			circleEffect.SetActive(true);
			if (timeButtonHolding > GameConst.TIME_HOLD_BIG_SLASH) {
				circleEffect.GetComponent<Animator>().SetTrigger("next");
			}
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			Chem();
			OnButtonDown();
		}

		if (Input.GetKeyUp(KeyCode.F)) {
			OnButtonUp();
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			LaunchKunai();
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			UseChuongSkill();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			UseDragonSkill();
		}

		if (Input.GetKeyDown(KeyCode.C)) {
			UseSpecialSkill();
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			finishSpecialSkill();
		}

		if (Input.GetKeyDown(KeyCode.U)) {
			DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -30);
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			DataPref.addNumData(GameConst.NUM_COLLECT_KEY, 30);
		}
	
		if (ItemData.TimeMagnetCount > 0) {
			if (!magnetObj.activeInHierarchy) magnetObj.SetActive(true);

			ItemData.AddTimeMagnet(-Time.deltaTime);
			if (ItemData.TimeMagnetCount <= 0) {
				Debug.Log("Het time magnet");
				magnetObj.SetActive(false);
			}
		}

	}

	void FixedUpdate() {
		if (isDead) {
			spriteRenderer.enabled = false;
			Reborn();
			return;
		}
//		else {
//			animator.SetBool("Reborn", false);
//		}

		if (isGameOver) return;

		if (upDown) {
			float t = transform.position.y - positionYBeforeUpDown;
			if (Mathf.Abs(t) < 1.5f) {
				transform.position = new Vector3(transform.position.x, transform.position.y + signUpDown*10*Time.deltaTime, transform.position.z);
			} else {
				signUpDown *= -1;
				transform.position = new Vector3(transform.position.x, transform.position.y + signUpDown*10*Time.deltaTime, transform.position.z);
			}
		}

		if (animator.GetInteger("Died") != 0) Chay (0, 0); 
		else {

			bool right = Physics2D.OverlapCircle(rightCheck.position, groundRadius, whatIsGround);

			if (right && !special) {
				Debug.Log("Den day roi");
				Chay(0, rigid2D.velocity.y);
			} else

			Chay(speed, rigid2D.velocity.y);
		}

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		animator.SetBool("Ground", grounded);

		if (grounded) {
			doubleJump = false;
			currentPosition = this.transform.position;
		}
		animator.SetBool("DoubleJump", doubleJump);
		float verY = rigid2D.velocity.y;
		animator.SetFloat("verY", verY);

		if (finishedChem) {
			FinishChem();
		}

		if (finishedSuriken) {
			FinishSuriken();
		}
	}

	void OutOfHP() {
		if (!isDead) SoundManager.instance.PlaySingleByName(GameConst.PLAYER_DEAD_AUDIO);

		if (grounded) animator.SetInteger("Died", 1);
		else {
			animator.SetInteger("Died", 2);
		}
	}

	void Reborn () {

		if (ItemData.UpCount > 0) {
			isGameOver = false;

			if (!grounded)
				transform.position = new Vector3(transform.position.x, camController.transform.position.y + 3, transform.position.z);

			spriteRenderer.enabled = true;
			col2D.enabled = true;
			animator.SetInteger("Died", 0);
			animator.SetTrigger("Reborn");

			battu = true;
			rebornEffect.SetActive(true);

			GameObject bbb = Instantiate(BaKunai, transform.position, Quaternion.identity) as GameObject;
			bbb.GetComponent<Rigidbody2D>().velocity = new Vector3(kunaiSpeed, 0, 0);

			CancelInvoke("HetBatTu");
			Invoke("HetBatTu", 4);

			hpController.IncreaseProcess(100);
			isDead = false;
			ItemData.AddUp(-1);
			saved = false;
		} else
			GameOver();
	}

	void HetBatTu() {
		battu = false;
		rebornEffect.SetActive(false);
	}

	void GameOver () {
		if (!GameConst.IS_TEST) {
//			Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
			SoundManager.instance.ShowBanner();
		}

		camUp.ScrollSpeed = 0;
		camFront.ScrollSpeed = 0;

		isGameOver = true;
		gameOverCanvas.transform.parent.gameObject.SetActive(true);
		gameOverCanvas.GetComponent<Animator>().SetBool("gameover", true);

		if (!saved) {
			timeGoToGOV += Time.deltaTime;
			if (timeGoToGOV > 5) {
				SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
				timeGoToGOV = 0;
			}
		} else {
			timeGoToGOV = 0;
		}
	}

	void Chay(float vSpeed, float hSpeed) {
		rigid2D.velocity = new Vector2(vSpeed, hSpeed);
	}

	public void OnButtonDown() {
		ButtonHolding = true;
	}

	public void OnButtonUp() {

		if (special) {
			timeButtonHolding = 0;
			circleEffect.SetActive(false);
			ButtonHolding = false;
			return;
		}
		ButtonHolding = false;

		if (timeButtonHolding >= GameConst.TIME_HOLD_BIG_SLASH) {
			LaunchBigSlash();
		} else if (timeButtonHolding >= GameConst.TIME_HOLD_BIG_KUNAI) {
			LaunchBigKunai();
		}

		timeButtonHolding = 0;
		circleEffect.SetActive(false);
	}

	public void Chem() {
		if (special) return;
		SoundManager.instance.PlaySingleByName(GameConst.PLAYER_CHEM_AUDIO);

		if (!isChem1 && !isChem2) {
			animator.SetTrigger("SwordAttack");
			animator.SetBool("Return1", true);
		} else if (isChem1 && !isChem2) {
			animator.SetTrigger("SwordAttack2");
			animator.SetBool("Return1", false);
			animator.SetBool("Return2", true);
		} else if (isChem2) {
			animator.SetTrigger("SwordAttack3");
			animator.SetBool("Return2", false);
			animator.SetBool("Return1", false);
//			animator.SetBool("Return3", true);
		}

//		Vector3 offset = new Vector3(2.5f, 0, 0);

//		if (ItemData.IsUpgradedSlash) {
//			GameObject sl = Instantiate(superSlash, transform.position + offset, Quaternion.identity) as GameObject;
//			sl.transform.parent = transform;
//		} else {
//			GameObject sl = Instantiate(slash, transform.position + offset, Quaternion.identity) as GameObject;
//			sl.transform.parent = transform;
//		}
		//Invoke("FinishChem", 0.9f);
	}

	void FinishChem() {
//		animator.SetBool("FinishChem", true);
	}

	void FinishSuriken() {
		animator.SetBool("SurikenAttack", false);
	}

	public void LaunchKunai() {
		if (special) return;
		if (ItemData.SurikenCount <= 0) return;
	
		SoundManager.instance.PlaySingleByName(GameConst.PHI_TIEU_AUDIO);

		if (grounded) animator.SetTrigger("SurikenAttack");

		ItemData.AddSuriken(-1);

		Vector3 offset = new Vector3(0.7f, 1f, 0);
		GameObject phitieu = Instantiate(kunai, transform.position + offset, Quaternion.identity) as GameObject;

		phitieu.GetComponent<Rigidbody2D>().velocity = new Vector3(kunaiSpeed, 0, 0);
	}

	public void UseDragonSkill() {
		if (special || cantDragon) return;
		if (ItemData.DragonCount <= 0) return;

		SoundManager.instance.PlaySingleByName(GameConst.DRAGON_AUDIO);
		cantDragon = true;
		ItemData.AddDragon(-1);

		Vector3 pos = new Vector3(transform.position.x - 10, 0, transform.position.z);
		Instantiate(dragonSkill, pos, Quaternion.identity);
		Invoke("CanDragon", 2.0f);
	}

	void CanChuong() {
		cantChuong = false;
	}

	public void UseChuongSkill() {
		if (special || cantChuong) return;
		if (ItemData.ChuongCount <= 0) return;

		SoundManager.instance.PlaySingleByName(GameConst.CHUONG_AUDIO);
		cantChuong = true;
		ItemData.AddChuong(-1);

		if (grounded) animator.SetTrigger("Chuong");
		Vector3 pos = new Vector3(transform.position.x + GameConst.CHUONG_OFFSET_X, transform.position.y + GameConst.CHUONG_OFFSET_Y, transform.position.z);
		Instantiate(chuongSkill, pos, Quaternion.identity);
		Invoke("CanChuong", 1);
	}

	void CanDragon() {
		cantDragon = false;
	}

	public void LaunchBigSlash() {
		if (special) return;

		Vector3 offset = new Vector3(2, 1.2f, 0);
		GameObject phitieu = Instantiate(bigSlash, transform.position + offset, Quaternion.identity) as GameObject;
		
		phitieu.GetComponent<Rigidbody2D>().velocity = new Vector3(kunaiSpeed, 0, 0);
	}

	public void LaunchBigKunai() {
		if (special) return;

		Vector3 offset = new Vector3(1, 1.2f, 0);
		GameObject phitieu = Instantiate(bigKunai, transform.position + offset, Quaternion.identity) as GameObject;
		
		phitieu.GetComponent<Rigidbody2D>().velocity = new Vector3(kunaiSpeed, 0, 0);
	}

	public void Jump() {
		if (special) return;

		if (grounded || !doubleJump) {

			rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpHeight);
			animator.SetBool("Ground", false);

			if (!doubleJump && !grounded) {
				doubleJump = true;
				animator.SetBool("DoubleJump", true);
			}
		}
	}

	public Vector3 GetOffsetWithCurrent() {
		return transform.position - currentPosition;
	}

	public void beAttacked(int damage) {
		if (special) return;

		if (damage < 100 && battu == true) {
			
		} else{
			hpController.DecreaseProcess(damage);
			SoundManager.instance.Vibrate();
			if (Mathf.Abs(hpController.GetProgress()) < 0.1f) {
				OutOfHP();
			} else {
				SoundManager.instance.PlaySingleByName(GameConst.PLAYER_INJURE_AUDIO);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			float groundPositionY = coll.gameObject.transform.position.y;
			camController.SetPositionY(groundPositionY + GameConst.DIS_GROUND_CAM);
		}
	}

	public void UseSpecialSkill() {
        special = true;
        animator.SetBool("special", true);
        camController.setSpecial(1);
        rigid2D.isKinematic = true;

        specialObj.SetActive(true);
        speed = speed * 1.5f;
        Invoke("finishSpecialSkill", 0.1f);
	}

	public void finishSpecialSkill() {
		speed = speed / 1.5f;
		upDown = false;
		camController.setSpecial(2);

		specialObj.SetActive(false);
	}

	public void afterFinishSpecialSkill() {
		special = false;
		animator.SetBool("special", false);
		rigid2D.isKinematic = false;
	}

	public void Collect (Collectible item) {
		item.BeCollected();
	}

	public void UpDownSpecial(){
		if (upDown) return;
		upDown = true;
		positionYBeforeUpDown = transform.position.y;
//		Debug.Log("positionYBeforeUpDown: " + positionYBeforeUpDown);
	}

	public void Save() {
		camUp.ScrollSpeed = 5;
		camFront.ScrollSpeed = 10;
		timeGoToGOV = 0;
		saved = true;
		isGameOver = false;

		Time.timeScale = 1;
		Chay(speed, rigid2D.velocity.y);
		ItemData.AddUp(1);
		Reborn();
	}

}
