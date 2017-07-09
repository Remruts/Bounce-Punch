using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbondodgeScript : dodgeScript {

	Animator myAnim;

	void Start(){
		myAnim = GetComponent<Animator> ();
	}

	override public void dodge(){
		myAnim.SetTrigger ("dodge");
	}
}
