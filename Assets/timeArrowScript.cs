using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeArrowScript : buttonScript {

	public bool left = false;

	override public void press(){
		if (left){
			settingsScript.settings.matchTime -= 30;
			if (settingsScript.settings.matchTime < 30){
				settingsScript.settings.matchTime = 990;
			}
		} else{
			settingsScript.settings.matchTime += 30;
			if (settingsScript.settings.matchTime > 990){
				settingsScript.settings.matchTime = 30;
			}
		}
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
		spr.color = Color.gray;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}

}
