using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButtonScript : buttonScript {

	public string level = "mainMenuScene";

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
		transitionScript.transition.level = level;
		transitionScript.transition.startTransition(0.25f);
	}
}
