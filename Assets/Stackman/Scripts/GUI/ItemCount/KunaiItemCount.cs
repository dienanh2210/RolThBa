using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KunaiItemCount : ItemCount {
	#region implemented abstract members of ItemCount

	protected override int GetItemCount () {
		return ItemData.SurikenCount;
	}

	#endregion


}
