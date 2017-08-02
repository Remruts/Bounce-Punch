using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToScript : buttonScript {

	public string scene;

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
		transitionScript.transition.setTransition(scene);
		transitionScript.transition.startTransition(0.25f);
	}
}
