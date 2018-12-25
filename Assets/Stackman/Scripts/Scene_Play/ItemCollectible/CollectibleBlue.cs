using UnityEngine;
using System.Collections;

public class CollectibleBlue : Collectible {

	#region implemented abstract members of Collectible
	
	protected override void InitBarAndType ()
	{
		bar = GameObject.Find("BlueFire_mid").GetComponent<BlueFireMid>();
	}
	
	#endregion

	#region implemented abstract members of Collectible

	protected override void AddItem ()
	{
		ItemData.AddChuong(1);
	}

	#endregion
}
