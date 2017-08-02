﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour {

	public SpriteRenderer[] buttons;
	public buttonScript[] scripts;
	int selected = 0;
	bool canMove = true;

	AudioSource audioSource;
	public AudioClip selectSound;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

		for (int j=1; j<5; ++j){
			if (canMove){
				float axis = Input.GetAxis("j"+j+"Vertical");
				if (Mathf.Abs(axis) > 0.05){
					selected += Mathf.RoundToInt(Mathf.Sign(axis));
					canMove = false;
					Invoke("reset", 0.2f);

					if (selected >= buttons.Length){
						selected = 0;
					}
					if (selected < 0){
						selected = buttons.Length - 1;
					}

					for (int i = 0; i < buttons.Length; ++i){
						buttons[i].color = Color.white;
					}

					audioSource.PlayOneShot(selectSound, settingsScript.settings.soundVolume);
				}
			}
			if (Input.GetButtonDown ("j"+j+"Attack")){
				scripts[selected].press();
			}
		}

		buttons[selected].color = Color.black;

	}

	void reset(){
		canMove = true;
	}
}
