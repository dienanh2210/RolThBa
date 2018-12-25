using UnityEngine;
using System.Collections;

public class RedFireMid : ProgressBar {

	#region implemented abstract members of ProgressBar

	protected override void InitProcessAndUnit () {
		progress = 0.01f;
		unit = 0.01f;
	}

	#endregion


}
