using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerButtonScript : buttonScript {

	public enum Button {Attack, Block, Dodge, Special, Start, CW, CCW, CWSlow, CCWSlow};
	public Button button;
	public TextMesh txt;
	int id = 1;

	bool pressed = false;

	string buttonString = "";

	void Update(){
		id = transform.parent.gameObject.GetComponent<buttonsIdScript>().id;

		switch(button){
		case Button.Attack:
			buttonString = inputManager.inputman.attackButton[id-1];
		break;
		case Button.Block:
			buttonString = inputManager.inputman.blockButton[id-1];
		break;
		case Button.Dodge:
			buttonString = inputManager.inputman.dodgeButton[id-1];
		break;
		case Button.Special:
			buttonString = inputManager.inputman.specialButton[id-1];
		break;
		case Button.Start:
			buttonString = inputManager.inputman.startButton[id-1];
		break;
		case Button.CW:
			buttonString = inputManager.inputman.CWButton[id-1];
		break;
		case Button.CCW:
			buttonString = inputManager.inputman.CCWButton[id-1];
		break;
		case Button.CWSlow:
			buttonString = inputManager.inputman.CWSlowButton[id-1];
		break;
		case Button.CCWSlow:
			buttonString = inputManager.inputman.CCWSlowButton[id-1];
		break;
		}

		if (buttonString.Length > 11){
			txt.text = buttonString.Substring(11);
		}
	}

	void OnMouseExit(){
		if (!pressed){
			spr.color = Color.white;
		}
	}

	override public void press(){
		audioSource.PlayOneShot(pressSound, settingsScript.settings.soundVolume);

		spr.color = Color.black;
		StopCoroutine(GetButton());
		StartCoroutine(GetButton());
		pressed = true;
	}

	IEnumerator GetButton(){
		yield return new WaitForSeconds(0.1f);
		bool finished = false;
		while (!finished){
			for (int j = 0; j < 20; ++j){
				for (int i = 1; i < 5; ++i){
					if (Input.inputString != ""){
						buttonString = Input.inputString;
						spr.color = Color.white;

						switch(button){
						case Button.Attack:
							inputManager.inputman.setAttack(id-1, buttonString);
						break;
						case Button.Block:
							inputManager.inputman.setBlock(id-1, buttonString);
						break;
						case Button.Dodge:
							inputManager.inputman.setDodge(id-1, buttonString);
						break;
						case Button.Special:
							inputManager.inputman.setSpecial(id-1, buttonString);
						break;
						case Button.Start:
							inputManager.inputman.setStart(id-1, buttonString);
						break;
						case Button.CW:
							inputManager.inputman.setCW(id-1, buttonString);
						break;
						case Button.CCW:
							inputManager.inputman.setCCW(id-1, buttonString);
						break;
						case Button.CWSlow:
							inputManager.inputman.setCWSlow(id-1, buttonString);
						break;
						case Button.CCWSlow:
							inputManager.inputman.setCCWSlow(id-1, buttonString);
						break;
						}

						finished = true;
						pressed = false;
						break;
					}
					if (Input.GetKeyDown ("joystick " + i + " button " + j)){
						buttonString = "joystick " + id +" button " + j;
						spr.color = Color.white;

						switch(button){
						case Button.Attack:
							inputManager.inputman.setAttack(id-1, buttonString);
						break;
						case Button.Block:
							inputManager.inputman.setBlock(id-1, buttonString);
						break;
						case Button.Dodge:
							inputManager.inputman.setDodge(id-1, buttonString);
						break;
						case Button.Special:
							inputManager.inputman.setSpecial(id-1, buttonString);
						break;
						case Button.Start:
							inputManager.inputman.setStart(id-1, buttonString);
						break;
						case Button.CW:
							inputManager.inputman.setCW(id-1, buttonString);
						break;
						case Button.CCW:
							inputManager.inputman.setCCW(id-1, buttonString);
						break;
						case Button.CWSlow:
							inputManager.inputman.setCWSlow(id-1, buttonString);
						break;
						case Button.CCWSlow:
							inputManager.inputman.setCCWSlow(id-1, buttonString);
						break;
						}
						finished = true;
						pressed = false;
						break;
					}
				}
			}
			yield return null;
		}
		inputManager.inputman.Save();
	}

}
