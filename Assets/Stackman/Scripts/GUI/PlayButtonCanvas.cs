using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButtonCanvas : MonoBehaviour {
		
	public GameObject chuongButton;
	public GameObject dragonButton;
	public GameObject surikenButton;

	
	void Update () {
			
		if (ItemData.ChuongCount <= 0) {
			chuongButton.GetComponent<Image>().color = new Color32(92, 92, 92, 255);
		} else {
			chuongButton.GetComponent<Image>().color = Color.white;
		}

		if (ItemData.DragonCount <= 0) {
			dragonButton.GetComponent<Image>().color = new Color32(92, 92, 92, 255);
		} else {
			dragonButton.GetComponent<Image>().color = Color.white;
		}

		if (ItemData.SurikenCount <= 0) {
			surikenButton.GetComponent<Image>().color = new Color32(92, 92, 92, 255);
		} else {
			surikenButton.GetComponent<Image>().color = Color.white;
		}

	}
}
