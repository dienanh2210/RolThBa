using UnityEngine;
using System.Collections;

public class UpItemCount : ItemCount {
	#region implemented abstract members of ItemCount

	protected override int GetItemCount () {
		return ItemData.UpCount;
	}

	#endregion
}
