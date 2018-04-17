using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetButtonScript : buttonScript {
	int id = 1;

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		id = transform.parent.gameObject.GetComponent<buttonsIdScript>().id;
		inputManager.inputman.resetButtons(id - 1);
		spr.color = Color.black;
		Invoke("reset", 0.2f);
		inputManager.inputman.Save();
	}

	void reset(){
		spr.color = Color.white;
	}
}
