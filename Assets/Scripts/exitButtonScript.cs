using UnityEngine;
using System.Collections;

public class exitButtonScript : buttonScript {
	
	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
		Debug.Log ("Exiting game...");
		Application.Quit();
	}
}
