using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButtonScript : MonoBehaviour {

	public string level = "mainMenuScene";

	void OnMouseDown(){
		transitionScript.transition.level = level;
		transitionScript.transition.startTransition(2f);
	}
}
