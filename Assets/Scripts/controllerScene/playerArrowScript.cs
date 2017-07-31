using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerArrowScript : MonoBehaviour {
	public controllerHandSelectorScript scr;
	public TextMesh playerText;
	public GameObject buttons;

	public bool left = false;

	SpriteRenderer spr;
	void Start(){
		spr = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown(){
		press();
	}

	public void press(){
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
