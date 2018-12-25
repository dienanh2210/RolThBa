using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuySurikenController : BuyController {

	public GameObject image;

	void Start () {
		CheckActive();
	}

	void Update () {
		CheckActive();
	}

	private void CheckActive() {
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < GameConst.SURIKEN_COST) {
			image.SetActive(true);
		} else {
			image.SetActive(false);
		}
	}

	protected override void BuyCompleted () {
		SoundManager.instance.AnalyticReport("Shop", "buy shuriken");
		ItemData.AddSuriken(30);
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -GameConst.SURIKEN_COST);
	}
}
