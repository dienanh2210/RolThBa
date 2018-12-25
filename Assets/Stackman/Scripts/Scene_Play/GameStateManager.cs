using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class GameStateManager : MonoBehaviour {

	public GameObject buttonCanvas;
	public GameObject shopCanvas;
	public GameObject image;
	public GameObject completeCanvas;
	public GameObject buyCanvas;

	void Start() {
		Time.timeScale = 1.05f;
		SoundManager.instance.HideBanner();
		SoundManager.instance.PlayMusic(GameConst.PLAY_MUSIC);

		SoundManager.instance.AnalyticReport("Play Scene", "Start button click");
	}

	public void OnPause() {
		if (!GameConst.IS_TEST) {
//			Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
			SoundManager.instance.ShowBanner();
		}

		Time.timeScale = 0;
		buttonCanvas.SetActive(false);
		image.SetActive(true);
	}

	public void OnUnpause() {
		if (!GameConst.IS_TEST) { 
//			Admob.Instance().removeBanner();
			SoundManager.instance.HideBanner();
		}

		Time.timeScale = 1;
		buttonCanvas.SetActive(true);
		image.SetActive(false);
	}

	public void ShowShop() {
		SoundManager.instance.AnalyticReport("Play Scene", "Show Shop button click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		shopCanvas.SetActive(true);
		OnPause();
	}

	public void CloseShop() {
		SoundManager.instance.AnalyticReport("Play Scene", "Close Shop button click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		shopCanvas.SetActive(false);
		completeCanvas.SetActive(false);
		buyCanvas.SetActive(true);

		OnUnpause();
	}

	public void GoToGameOver() {
		SoundManager.instance.AnalyticReport("Play Scene", "Game Over");
		ItemData.ResetCount();
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
	}

	public void GoToVictory() {
		SoundManager.instance.AnalyticReport("Play Scene", "Game Victory");
		ItemData.ResetCount();
		SceneManager.LoadScene("Victory", LoadSceneMode.Single);
	}

	public void GoToHome() {
		SoundManager.instance.AnalyticReport("Play Scene", "Home Button Click");

		Time.timeScale = 1;
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		if (!GameConst.IS_TEST) {

           // SG_AdManager.ads.RequestIntertitial();
           // SG_AdManager.ads.ShowIntertitial();

            SoundManager.instance.ShowFullAd();
		}
		SceneManager.LoadScene("Home", LoadSceneMode.Single);
		SoundManager.instance.Vibrate();
	}

	public void GoToHelp() {
		SoundManager.instance.AnalyticReport("Play Scene", "Replay Button Click");

		Time.timeScale = 1;
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		if (!GameConst.IS_TEST) {
           // SG_AdManager.ads.RequestIntertitial();
           //SG_AdManager.ads.ShowIntertitial();
            SoundManager.instance.ShowFullAd();
		}
		//		string name = HomeStateManager.GetSceneName();
		SceneManager.LoadScene("Help", LoadSceneMode.Single);
		SoundManager.instance.Vibrate();
	}

}
