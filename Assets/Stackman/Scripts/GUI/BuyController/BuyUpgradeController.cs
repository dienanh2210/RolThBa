using UnityEngine;
using System.Collections;

public class BuyUpgradeController : BuyController {

	public GameObject image;
	
	// Use this for initialization
	void Start () {
		//		image.SetActive(true);
		
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.UPGRADE_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}

	void Update () {
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.UPGRADE_COST || ItemData.IsUpgradedSlash == true) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}

	#region implemented abstract members of BuyController

	protected override void BuyCompleted () {
		SoundManager.instance.AnalyticReport("Shop", "buy Upgrade");
		ItemData.IsUpgradedSlash = true;
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -GameConst.UPGRADE_COST);
	}

	#endregion
}
