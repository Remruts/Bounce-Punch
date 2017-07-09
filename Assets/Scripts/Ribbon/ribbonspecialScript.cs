using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonspecialScript : specialScript {

	Animator myAnim;
	public GameObject sparks;

	void Start () {
		myAnim = GetComponent<Animator> ();
	}

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		myAnim.SetTrigger ("special");

		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

	}
}
