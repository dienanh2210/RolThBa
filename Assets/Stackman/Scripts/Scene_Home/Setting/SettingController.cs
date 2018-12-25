using UnityEngine;
using System.Collections;

public class SettingController : MonoBehaviour {

	//0: ON
	//1: OFF

	public GameObject music_on;
	public GameObject music_off;
	public GameObject sound_on;
	public GameObject sound_off;
	public GameObject vibrate_on;
	public GameObject vibrate_off;

	void Start() {
		if (DataPref.getNumData(GameConst.MUSIC_KEY) == 1) {
			music_on.SetActive(false);
			music_off.SetActive(true);
		} else {
			music_on.SetActive(true);
			music_off.SetActive(false);
		}

		if (DataPref.getNumData(GameConst.SOUND_KEY) == 1) {
			sound_on.SetActive(false);
			sound_off.SetActive(true);
		} else {
			sound_on.SetActive(true);
			sound_off.SetActive(false);
		}

		if (DataPref.getNumData(GameConst.VIBRATE_KEY) == 1) {
			vibrate_on.SetActive(false);
			vibrate_off.SetActive(true);
		} else {
			vibrate_on.SetActive(true);
			vibrate_off.SetActive(false);
		}
	}

	public void OnMusicClick() {
		if (DataPref.getNumData(GameConst.MUSIC_KEY) == 0) {
			DataPref.setNumData(GameConst.MUSIC_KEY, 1);
			music_on.SetActive(false);
			music_off.SetActive(true);
			SoundManager.instance.StopMusic();
		} else {
			DataPref.setNumData(GameConst.MUSIC_KEY, 0);
			music_on.SetActive(true);
			music_off.SetActive(false);
			SoundManager.instance.PlayMusic(GameConst.HOME_MUSIC);
		}
	}

	public void OnSoundClick() {
		if (DataPref.getNumData(GameConst.SOUND_KEY) == 0) {
			DataPref.setNumData(GameConst.SOUND_KEY, 1);
			sound_on.SetActive(false);
			sound_off.SetActive(true);
		} else {
			DataPref.setNumData(GameConst.SOUND_KEY, 0);
			sound_on.SetActive(true);
			sound_off.SetActive(false);
		}
	}

	public void OnVibrateClick() {
		if (DataPref.getNumData(GameConst.VIBRATE_KEY) == 0) {
			DataPref.setNumData(GameConst.VIBRATE_KEY, 1);
			vibrate_on.SetActive(false);
			vibrate_off.SetActive(true);
		} else {
			DataPref.setNumData(GameConst.VIBRATE_KEY, 0);
			vibrate_on.SetActive(true);
			vibrate_off.SetActive(false);
		}
	}

}
