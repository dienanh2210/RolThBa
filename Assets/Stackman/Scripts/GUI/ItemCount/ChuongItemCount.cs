using UnityEngine;
using System.Collections;

public class ChuongItemCount : ItemCount {
	#region implemented abstract members of ItemCount

	protected override int GetItemCount () {
		return ItemData.ChuongCount;
	}

	#endregion



}
