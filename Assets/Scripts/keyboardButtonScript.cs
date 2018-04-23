using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyboardButtonScript : buttonScript {

	public TextMesh txt;
	public GameObject[] directions;
	bool pressed = false;
	int id = 1;

	void OnMouseExit(){
		if (!pressed){
			spr.color = Color.white;
		}
	}

	void Update(){
		id = transform.parent.gameObject.GetComponent<buttonsIdScript>().id;
		txt.text = inputManager.inputman.keyboard[id-1] ? "ON" : "OFF";
		foreach (GameObject direction in directions){		
			direction.SetActive(inputManager.inputman.keyboard[id-1]);
		}
	}

	void Start(){
		base.Start();
		txt.text = inputManager.inputman.keyboard[id-1] ? "ON" : "OFF";
		foreach (GameObject direction in directions){
			direction.SetActive(inputManager.inputman.keyboard[id-1]);
		}

	}

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		spr.color = Color.black;
		inputManager.inputman.toggleKeyboard(id-1);
		pressed = true;
	}
}
