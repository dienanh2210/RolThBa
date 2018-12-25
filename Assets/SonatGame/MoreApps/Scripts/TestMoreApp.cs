using UnityEngine;
using System.Collections;

public class TestMoreApp : MonoBehaviour {

    public static bool isShowedMoreAppInHome = false;
    public GameObject moreAppView;
    public bool allowMoreAppAppearManyTime = false;

    // Use this for initialization
    void Start()
    {
        //LeanTween.delayedCall(3.6f, () =>
        //    {
        //        // More
        //        if (MoreAppController.instance.isLoaded && !(!allowMoreAppAppearManyTime && isShowedMoreAppInHome))
        //        {
        //            ShowMoreApp();
        //        }
        //    });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (moreAppView.activeSelf)
            {
                moreAppView.SetActive(false);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void ShowMoreApp()
    {
        MoreAppController.instance.ShowSmartMoreApp();
        isShowedMoreAppInHome = true;
    }
}
