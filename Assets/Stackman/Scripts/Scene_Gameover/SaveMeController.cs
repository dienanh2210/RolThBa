using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveMeController : MonoBehaviour {

	public PlayerController player;
	public GameObject gameOverCanvas;

	private Image image;
	private Button button;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	// Update is called once per frame
	void Update() {
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < ItemData.NumToSave) {
			image.color = Color.gray;
			button.enabled = false;
		} else {
			image.color = Color.white;
			button.enabled = true;
		}
	}

	public void OnClick () {
		if (DataPref.getNumData(GameConst.NUM_COLLECT_KEY) < ItemData.NumToSave) return;
		player.Save();
		gameOverCanvas.SetActive(false);
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -ItemData.NumToSave);
		ItemData.AddNumToSave(ItemData.NumToSave);
	}
}
