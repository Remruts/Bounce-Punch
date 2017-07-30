using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoButtonScript : MonoBehaviour {

	public Sprite autoSprite;
	public Sprite manualSprite;
	public int id;

	playerCardScript scr;
	SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		scr = transform.parent.GetComponent<playerCardScript>();
		id = scr.playerId;
		spr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (settingsScript.settings.autoPaddles[id-1]){
			spr.sprite = autoSprite;
		} else {
			spr.sprite = manualSprite;
		}
	}

	public void toggle(){
		settingsScript.settings.autoPaddles[id-1] = !settingsScript.settings.autoPaddles[id-1];
	}
}
