using UnityEngine;
using System.Collections;

public class DragonItemCount : ItemCount {

	#region implemented abstract members of ItemCount

	protected override int GetItemCount () {
		return ItemData.DragonCount;
	}

	#endregion

}
