using UnityEngine;
using System.Collections;

public class DataPref{

	public static void addNumData(string key, int num) {
		int cur = getNumData(key);
		cur += num;
		setNumData(key, cur);
	}

	public static void setNumData(string key, int num) {
		PlayerPrefs.SetInt(key, num);
	}

	public static int getNumData(string key) {
		return PlayerPrefs.GetInt(key, 0);
	}
}
