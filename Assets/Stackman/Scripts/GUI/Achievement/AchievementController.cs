using UnityEngine;
using System.Collections;

public class AchievementController : MonoBehaviour {

	public GameObject achievement1;
	public GameObject achievement2;
	public GameObject achievement3;
	public GameObject achievement4;
	public GameObject achievement5;

	private const int DISTANCE_FOR_ACHIEVEMENT1 = 2000;
	private const int DISTANCE_FOR_ACHIEVEMENT2 = 4000;
	private const int DISTANCE_FOR_ACHIEVEMENT3 = 8000;
	private const int DISTANCE_FOR_ACHIEVEMENT4 = 20000;
	private const int DISTANCE_FOR_ACHIEVEMENT5 = 50000;

	private int bestDistance;
	private int isWinGame;

	// Use this for initialization
	void Start () {
//		DataPref.setNumData(GameConst.BEST_DISTANCE_KEY, 0);

		bestDistance = DataPref.getNumData(GameConst.BEST_DISTANCE_KEY);
		isWinGame = DataPref.getNumData(GameConst.IS_WIN_KEY);

		SetAchievement1();
		SetAchievement2();
		SetAchievement3();
		SetAchievement4();
		SetAchievement5();
	}
	
	void SetAchievement1 () {
		if (bestDistance >= DISTANCE_FOR_ACHIEVEMENT1) 
			achievement1.SetActive(true);
		else achievement1.SetActive(false);
	}
	void SetAchievement2 () {
		if (bestDistance >= DISTANCE_FOR_ACHIEVEMENT2) 
			achievement2.SetActive(true);
		else achievement2.SetActive(false);
	}
	void SetAchievement3 () {
		if (bestDistance >= DISTANCE_FOR_ACHIEVEMENT3) 
			achievement3.SetActive(true);
		else achievement3.SetActive(false);
	}
	void SetAchievement4 () {
		if (bestDistance >= DISTANCE_FOR_ACHIEVEMENT4) 
			achievement4.SetActive(true);
		else achievement4.SetActive(false);
	}
	void SetAchievement5 () {
		if (bestDistance >= DISTANCE_FOR_ACHIEVEMENT5) 
			achievement5.SetActive(true);
		else achievement5.SetActive(false);
	}

}
