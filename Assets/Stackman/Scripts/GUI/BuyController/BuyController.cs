using UnityEngine;
using System.Collections;

public abstract class BuyController : MonoBehaviour {
	public GameObject completeCanvas;
	public GameObject buyCanvas;

	protected abstract void BuyCompleted();

	public void Buy() {
		buyCanvas.SetActive(false);
		completeCanvas.SetActive(true);

		SoundManager.instance.PlaySingleByName(GameConst.BUY_AUDIO);
		BuyCompleted();
	}

	public void Continue() {
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		completeCanvas.SetActive(false);
		buyCanvas.SetActive(true);
	}
}
