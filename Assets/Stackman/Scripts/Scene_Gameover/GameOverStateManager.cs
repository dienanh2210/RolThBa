using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class GameOverStateManager : MonoBehaviour {

	private string sceneName = "Gameover Scene";

	void Start(){
		Time.timeScale = 1;
		SoundManager.instance.PlaySingleByNameLoop(GameConst.COUNT_AUDIO);
		SoundManager.instance.PlaySingleByName(GameConst.JUMP_DOWN_AUDIO);
		SoundManager.instance.AnalyticReport(sceneName, "Here");
	}

	public void gotoHomeScene() {
		SoundManager.instance.StopSingleLoop();

		SoundManager.instance.AnalyticReport(sceneName, "Home button click");

		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		if (!GameConst.IS_TEST) {

            // SG_AdManager.ads.RequestIntertitial();
           //  SG_AdManager.ads.ShowIntertitial();
           
			SoundManager.instance.ShowFullAd();
		}
		SceneManager.LoadScene("Home", LoadSceneMode.Single);
		SoundManager.instance.Vibrate();
	}

	public void gotoPlayScene() {
		SoundManager.instance.StopSingleLoop();
		SoundManager.instance.AnalyticReport(sceneName, "Replay button click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		if (!GameConst.IS_TEST) {
            //SG_AdManager.ads.RequestIntertitial();
           // SG_AdManager.ads.ShowIntertitial();

            SoundManager.instance.ShowFullAd();
		}
//		string name = HomeStateManager.GetSceneName();
		SceneManager.LoadScene("Help", LoadSceneMode.Single);
		SoundManager.instance.Vibrate();
	}
}
