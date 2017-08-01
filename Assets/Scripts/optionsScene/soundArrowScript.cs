﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundArrowScript : buttonScript {

	public bool left = false;
	public TextMesh musicText;

	public AudioClip sound;
	AudioSource audioSource;

	SpriteRenderer spr;
	void Start(){
		spr = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update(){
		musicText.text = (Mathf.Round(100 * settingsScript.settings.soundVolume)).ToString();
	}

	override public void press(){
		if (left){
			settingsScript.settings.soundVolume -= 0.1f;
			if (settingsScript.settings.soundVolume < 0f){
				settingsScript.settings.soundVolume = 0f;
			}
		} else{
			settingsScript.settings.soundVolume += 0.1f;
			if (settingsScript.settings.soundVolume > 1f){
				settingsScript.settings.soundVolume = 1f;
			}
		}
		audioSource.PlayOneShot(sound, settingsScript.settings.soundVolume);
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