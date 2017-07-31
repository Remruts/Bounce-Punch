using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButtonScript : MonoBehaviour {

	void OnMouseDown(){
		transitionScript.transition.level = "mainMenuScene";
		transitionScript.transition.startTransition(2f);
	}
}
