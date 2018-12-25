using UnityEngine;
using System.Collections;

public class BossHPProgress : ProgressBar {
	#region implemented abstract members of ProgressBar
	protected override void InitProcessAndUnit (){
		progress = 1f;
		unit = 0.01f;
	}
	#endregion

}
