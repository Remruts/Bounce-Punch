using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicArrowScript : buttonScript {

	public bool left = false;
	public TextMesh musicText;

	void Update(){
		musicText.text = (Mathf.Round(100 * settingsScript.settings.musicVolume)).ToString();
	}

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		if (left){
			settingsScript.settings.musicVolume -= 0.1f;
			if (settingsScript.settings.musicVolume < 0f){
				settingsScript.settings.musicVolume = 0f;
			}
		} else{
			settingsScript.settings.musicVolume += 0.1f;
			if (settingsScript.settings.musicVolume > 1f){
				settingsScript.settings.musicVolume = 1f;
			}
		}
		spr.color = Color.gray;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}
}
