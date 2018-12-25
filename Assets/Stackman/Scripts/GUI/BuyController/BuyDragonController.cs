using UnityEngine;
using System.Collections;

public class BuyDragonController : BuyController {

	public GameObject image;
	
	// Use this for initialization
	void Start () {
		//		image.SetActive(true);
		
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.DRAGON_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}

	void Update () {
		//		image.SetActive(true);
		
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.DRAGON_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}
	
	#region implemented abstract members of BuyController
	protected override void BuyCompleted () {
		SoundManager.instance.AnalyticReport("Shop", "buy s-arrow");
		ItemData.AddDragon(1);
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -GameConst.DRAGON_COST);
	}
	#endregion
}
