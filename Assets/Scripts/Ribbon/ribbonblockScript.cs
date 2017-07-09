using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonblockScript : blockScript {

	Animator myAnim;

	void Start(){
		myAnim = GetComponent<Animator> ();
	}

	override public void block(){
		myAnim.SetTrigger ("block");
	}
}
