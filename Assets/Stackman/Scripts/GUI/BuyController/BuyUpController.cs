using UnityEngine;
using System.Collections;

public class BuyUpController : BuyController {

	public GameObject image;
	
	// Use this for initialization
	void Start () {
		//		image.SetActive(true);
		
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.UP_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}
	void Update () {
		//		image.SetActive(true);
		
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.UP_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}

	#region implemented abstract members of BuyController

	protected override void BuyCompleted () {
		SoundManager.instance.AnalyticReport("Shop", "buy Up");
		ItemData.AddUp(1);
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -GameConst.UP_COST);
	}

	#endregion
}
