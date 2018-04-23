using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerButtonScript : buttonScript {

	public enum Button {Attack, Block, Dodge, Special, Start, CW, CCW, CWSlow, CCWSlow, Up, Down, Left, Right};
	public Button button;
	public TextMesh txt;
	int id = 1;

	bool pressed = false;

	string buttonString = "";

	private string[] keys;

	void Start(){
		base.Start();
		keys = new string[] {
		 "backspace",
		 "delete",
		 "tab",
		 "clear",
		 "return",
		 "pause",
		 "escape",
		 "space",
		 "up",
		 "down",
		 "right",
		 "left",
		 "insert",
		 "home",
		 "end",
		 "page up",
		 "page down",
		 "f1",
		 "f2",
		 "f3",
		 "f4",
		 "f5",
		 "f6",
		 "f7",
		 "f8",
		 "f9",
		 "f10",
		 "f11",
		 "f12",
		 "f13",
		 "f14",
		 "f15",
		 "0",
		 "1",
		 "2",
		 "3",
		 "4",
		 "5",
		 "6",
		 "7",
		 "8",
		 "9",
		 "!",
		 "\"",
		 "#",
		 "$",
		 "&",
		 "'",
		 "(",
		 ")",
		 "*",
		 "+",
		 ",",
		 "-",
		 ".",
		 "/",
		 ":",
		 ";",
		 "<",
		 "=",
		 ">",
		 "?",
		 "@",
		 "[",
		 "\\",
		 "]",
		 "^",
		 "_",
		 "`",
		 "a",
		 "b",
		 "c",
		 "d",
		 "e",
		 "f",
		 "g",
		 "h",
		 "i",
		 "j",
		 "k",
		 "l",
		 "m",
		 "n",
		 "o",
		 "p",
		 "q",
		 "r",
		 "s",
		 "t",
		 "u",
		 "v",
		 "w",
		 "x",
		 "y",
		 "z",
		 "numlock",
		 "caps lock",
		 "scroll lock",
		 "right shift",
		 "left shift",
		 "right ctrl",
		 "left ctrl",
		 "right alt",
		 "left alt"
	 	};
	}


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
		case Button.Up:
			buttonString = inputManager.inputman.upButton[id-1];
		break;
		case Button.Down:
			buttonString = inputManager.inputman.downButton[id-1];
		break;
		case Button.Left:
			buttonString = inputManager.inputman.leftButton[id-1];
		break;
		case Button.Right:
			buttonString = inputManager.inputman.rightButton[id-1];
		break;
		}

		if (buttonString.Length > 11){
			txt.text = buttonString.Substring(11);
		} else {
			txt.text = buttonString;
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
		float t = Time.realtimeSinceStartup;
		while (!finished){

			if (Time.realtimeSinceStartup - t > 3f){
				finished = true;
				pressed = false;
				spr.color = Color.white;
			}

			if (inputManager.inputman.keyboard[id-1]){
				if (Input.anyKeyDown){
					foreach (string key in keys) {
				         if (Input.GetKeyDown(key)) {
				             buttonString = key;
				         }
					 }
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
					case Button.Up:
						inputManager.inputman.setUp(id-1, buttonString);
					break;
					case Button.Down:
						inputManager.inputman.setDown(id-1, buttonString);
					break;
					case Button.Left:
						inputManager.inputman.setLeft(id-1, buttonString);
					break;
					case Button.Right:
						inputManager.inputman.setRight(id-1, buttonString);
					break;
					}
					Debug.Log(inputManager.inputman.upButton[id-1]);
					finished = true;
					pressed = false;
					break;
				}
			} else {
				for (int j = 0; j < 20; ++j){
					for (int i = 1; i < 5; ++i){
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
			}

			yield return null;
		}
		inputManager.inputman.Save();
	}

}
