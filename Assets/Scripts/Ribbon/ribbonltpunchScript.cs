using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonltpunchScript : lightPunchScript {

	Animator myAnim;

	void Start(){		
		myAnim = GetComponent<Animator> ();
	}

	override public void ltPunch(){
		myAnim.SetTrigger ("ltpunch");
	}
}
