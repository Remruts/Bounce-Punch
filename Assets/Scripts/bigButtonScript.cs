using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigButtonScript : MonoBehaviour {

	public Sprite smallSprite;
	public Sprite bigSprite;
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
		if (settingsScript.settings.bigPaddles[id-1]){
			spr.sprite = bigSprite;
		} else {
			spr.sprite = smallSprite;
		}
	}

	public void toggle(){
		settingsScript.settings.bigPaddles[id-1] = !settingsScript.settings.bigPaddles[id-1];
	}
}
