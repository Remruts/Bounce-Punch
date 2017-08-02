using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButtonScript : buttonScript {

	public string level = "mainMenuScene";

	override public void press(){
		if (settingsScript.settings.getPlayerNumber() >= 2){
			audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
			transitionScript.transition.level = level;
			transitionScript.transition.startTransition(0.25f);
		}
	}
}
