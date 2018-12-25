using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using admob;

public class HelpStateManager : MonoBehaviour {

	public GameObject loadingText;
	private bool isBtnStartClicked;


	void Start() {
		isBtnStartClicked = false;
		if (!GameConst.IS_TEST) {
//			Admob.Instance().showBannerRelative(new AdSize(415, 55), AdPosition.BOTTOM_LEFT, 40);
			SoundManager.instance.ShowBanner2();
		}
		SoundManager.instance.AnalyticReport("Help Scene", "Here");
	}

	public void MoveToStartScene() {
		if (isBtnStartClicked) return;

		SoundManager.instance.AnalyticReport("Help Scene", "Start button click");
//		Debug.Log("click start button");
		isBtnStartClicked = true;

		SoundManager.instance.PlaySingleByName(GameConst.BUTTON_CLICK_AUDIO);
		SoundManager.instance.Vibrate();

		StartCoroutine(LoadNewScene());
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene() {

		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(0.5f);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.

		string name = HomeStateManager.GetSceneName();

		AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

		//		Debug.Log("Progress" + async.progress);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			loadingText.transform.localScale = new Vector3(async.progress, 1);
			yield return null;
		}

	}


}
