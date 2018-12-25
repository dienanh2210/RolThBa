using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
//using admob;
//using Facebook.Unity;

public class HomeStateManager : MonoBehaviour {
	public GameObject shopCanvas;
	public GameObject achievementCanvas;
	public GameObject settingCanvas;
	public GameObject loadingText;
	public GameObject completeCanvas;
	public GameObject buyCanvas;
	private String sceneName = "Home Scene";

	private static int gameCount;

	void Awake() {
		
	}

	void Start() {
		SoundManager.instance.PlayMusic(GameConst.HOME_MUSIC);
		SoundManager.instance.AnalyticReport(sceneName, "Start Game");
		if (!GameConst.IS_TEST) {
			SoundManager.instance.ShowBanner();		
		}
	}

	public void ShowSetting() {
		SoundManager.instance.AnalyticReport(sceneName, "Setting Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		settingCanvas.SetActive(true);
		OnPause();
	}

	public void CloseSetting() {
		SoundManager.instance.AnalyticReport(sceneName, "Close Setting Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		settingCanvas.SetActive(false);
		OnUnpause();
	}

	public void ShowShop() {
		SoundManager.instance.AnalyticReport(sceneName, "Show Shop Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		shopCanvas.SetActive(true);
		OnPause();
	}
	
	public void CloseShop() {
		SoundManager.instance.AnalyticReport(sceneName, "Close Shop Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		shopCanvas.SetActive(false);
		completeCanvas.SetActive(false);
		buyCanvas.SetActive(true);
		OnUnpause();
	}

	public void ShowAchievement() {
		SoundManager.instance.AnalyticReport(sceneName, "Show Achievement Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		achievementCanvas.SetActive(true);
		OnPause();
	}

	public void CloseAchievement() {
		SoundManager.instance.AnalyticReport(sceneName, "Close Achievement Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		achievementCanvas.SetActive(false);
		OnUnpause();
	}

	public void Share() {
		SoundManager.instance.AnalyticReport(sceneName, "Share Button Click");
		SoundManager.instance.Vibrate();

	}

	private void ShareCallback ( ) {
		//if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
		//	Debug.Log("ShareLink Error: "+result.Error);
		//} else if (!String.IsNullOrEmpty(result.PostId)) {
		//	// Print post identifier of the shared content
		//	Debug.Log(result.PostId);
		//} else {
		//	// Share succeeded without postID
		//	Debug.Log("ShareLink success!");
		//}
	}

	public void GoToHelpScene() {
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		SceneManager.LoadScene("Help", LoadSceneMode.Single);
	}

	public void GoToStoryScene() {
		SoundManager.instance.AnalyticReport(sceneName, "Start Game Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		SceneManager.LoadScene("Story", LoadSceneMode.Single);
	}

	public void ExitGame() {
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();
		Application.Quit();
	}

    //	void Update() {
    //		if (Input.GetKeyDown(KeyCode.U)) {
    //			DataPref.addNumData(GameConst.NUM_COLLECT_KEY, -30);
    //		}
    //		
    //		if (Input.GetKeyDown(KeyCode.I)) {
    //			DataPref.addNumData(GameConst.NUM_COLLECT_KEY, 30);
    //		}
    //	}
    public void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

        }


    }

    public void OnPause() {
		Time.timeScale = 0;
	}

	public void OnUnpause() {
		Time.timeScale = 1;
	}

	public static string GetSceneName() {
		int a = gameCount;
		gameCount += 1;
		if (gameCount == 5) gameCount = 0;
		return "Play" + a;
	}

	//private const string FACEBOOK_APP_ID = "796747923679740";
	//private const string FACEBOOK_URL = "http://www.facebook.com/dialog/feed";


//	void ShareToFacebook (string linkParameter, string nameParameter, string captionParameter, string descriptionParameter, string pictureParameter, string redirectParameter)
//	{
//		Application.OpenURL (FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID +
//			"&link=" + WWW.EscapeURL(linkParameter) +
//			"&name=" + WWW.EscapeURL(nameParameter) +
//			"&caption=" + WWW.EscapeURL(captionParameter) + 
//			"&description=" + WWW.EscapeURL(descriptionParameter) + 
//			"&picture=" + WWW.EscapeURL(pictureParameter) + 
//			"&redirect_uri=" + WWW.EscapeURL(redirectParameter));
//	}


	void onInterstitialEvent(string eventName, string msg)
	{
		
	}

	void onBannerEvent(string eventName, string msg) {
		
	}

	public void OnShowLeaderBoardClick() {
		SoundManager.instance.AnalyticReport(sceneName, "Show Leaderboard Button Click");

		if (!GameConst.IS_TEST) {
			int bestDistance = DataPref.getNumData(GameConst.BEST_DISTANCE_KEY);
			Social.ReportScore(bestDistance, "CgkI3tfW-NMLEAIQBQ", (bool success) => {
				Debug.Log("status post score: " + success);
				SoundManager.instance.ShowLeaderBoard();
			});
		}

	}

  



}
