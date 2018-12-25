using UnityEngine;
using System.Collections;

public class Slash1Controller : MonoBehaviour {

	public GameObject chem1;
	public GameObject chem1nc;

	void OnEnable() {
		if (ItemData.IsUpgradedSlash) {
			chem1nc.SetActive(true);
			chem1.SetActive(false);
		} else {
			chem1.SetActive(true);
			chem1nc.SetActive(false);
		}
	}
}
