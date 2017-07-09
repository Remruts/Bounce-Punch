using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonhvpunchScript : heavyPunchScript {

	Animator myAnim;
	public GameObject sparks;

	void Start () {
		myAnim = GetComponent<Animator> ();
	}

	override public void hvPunch(){

		myAnim.SetTrigger ("hvpunch");
		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

	}

}
