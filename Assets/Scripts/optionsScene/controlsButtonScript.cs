using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlsButtonScript : buttonScript {

	SpriteRenderer spr;
	void Start(){
		spr = GetComponent<SpriteRenderer>();
	}

	override public void press(){
		transitionScript.transition.level = "controllerScene";
		transitionScript.transition.startTransition(2f);
		spr.color = Color.black;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}

	void OnMouseDown(){
		press();
	}
}
