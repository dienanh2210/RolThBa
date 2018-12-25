using UnityEngine;
using System.Collections;

public class EnemyWalkController : Enemy {

	protected override void InitHP() {
		HP = 10;
	}

	protected override void InitSpeed() {
		speed = GameConst.ENEMY_WALK_SPEED;
	}

	protected override void Move() {
		rigid2D.velocity = new Vector2(- speed, rigid2D.velocity.y);
	}


	#region implemented abstract members of Enemy
	protected override void Bonus ()
	{
		if (fires.Length > 0) {
			Vector3 pos = transform.position + new Vector3(1.5f, 1.3f, 0);
			int r = Random.Range(0, fires.Length);
			if (r == 0) {
				Instantiate(fires[0], pos, Quaternion.identity);	
			}
			else {
				Instantiate(fires[1], pos, Quaternion.identity);	
			}
		}
	}
	#endregion
}
