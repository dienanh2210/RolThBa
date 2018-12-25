using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class StoryStateManager : MonoBehaviour {

	void Start() {
		SoundManager.instance.AnalyticReport("Story Scene", "Here");

		SoundManager.instance.PlayMusic(GameConst.STORY_MUSIC);
		if (!GameConst.IS_TEST) {
//			Admob.Instance().removeBanner();
			SoundManager.instance.HideBanner();
		}
	}
    public void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Home", LoadSceneMode.Single);
            }
            
        }


    }
    public void GoToHelpScene() {
		SoundManager.instance.AnalyticReport("Story Scene", "Play Button Click");
		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();

		SceneManager.LoadScene("Help", LoadSceneMode.Single);
	}

}
