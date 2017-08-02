using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlsButtonScript : buttonScript {

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		transitionScript.transition.level = "controllerScene";
		transitionScript.transition.startTransition(0.25f);
		spr.color = Color.black;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}
}
