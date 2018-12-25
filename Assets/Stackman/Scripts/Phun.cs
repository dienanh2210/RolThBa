using UnityEngine;
using System.Collections;

public class Phun : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		PhunLua();
	}
	
	void PhunLua() {
		int r = Random.Range(2, 3);

		animator.SetTrigger("phun");

		Invoke("PhunLua", r);

	}	
}
