using UnityEngine;
using System.Collections;

public class TempScript : MonoBehaviour {
		
	public void AddGem() {
		DataPref.addNumData(GameConst.NUM_COLLECT_KEY, 100);
	}


}
