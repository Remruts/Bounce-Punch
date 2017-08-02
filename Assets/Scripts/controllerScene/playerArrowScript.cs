using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerArrowScript : buttonScript {
	public controllerHandSelectorScript scr;
	public TextMesh playerText;
	public GameObject buttons;

	public bool left = false;

	override public void press(){

		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		if (left){
			scr.playerId -= 1;
			if (scr.playerId < 1){
				scr.playerId = 4;
			}
		} else {
			scr.playerId += 1;
			if (scr.playerId > 4){
				scr.playerId = 1;
			}
		}
		spr.color = Color.gray;
		buttons.GetComponent<buttonsIdScript>().id = scr.playerId;
		playerText.text = "Player " + scr.playerId;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}
}
