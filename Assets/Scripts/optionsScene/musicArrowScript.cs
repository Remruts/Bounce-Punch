using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicArrowScript : buttonScript {

	public bool left = false;
	public TextMesh musicText;

	SpriteRenderer spr;
	void Start(){
		spr = GetComponent<SpriteRenderer>();
	}

	void Update(){
		musicText.text = (Mathf.Round(100 * settingsScript.settings.musicVolume)).ToString();
	}

	override public void press(){
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

	void OnMouseDown(){
		press();
	}
}
