using UnityEngine;
using System.Collections;

public class SlashController : MonoBehaviour {

	public bool finishSlash;
	
	void Update () {
		if (finishSlash) gameObject.SetActive(false);
	}

}
