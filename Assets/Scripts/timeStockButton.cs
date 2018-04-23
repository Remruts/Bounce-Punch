using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeStockButton : buttonScript {

	TextMesh txt;
	void Start(){
		audioSource = GetComponent<AudioSource>();
		txt = GetComponent<TextMesh>();
	}
	void Update(){
		if (settingsScript.settings.stockBattle){
			txt.text = "Stock";
		} else {
			txt.text = "Time";
		}
	}

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);
		settingsScript.settings.stockBattle = !settingsScript.settings.stockBattle;

	}
}
