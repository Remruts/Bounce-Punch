using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonltpunchScript : lightPunchScript {

	Animator myAnim;
	public AudioClip swishsound;
	AudioSource audioSource;

	void Start(){
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	override public void ltPunch(){
		myAnim.SetTrigger ("ltpunch");
		audioSource.PlayOneShot(swishsound, settingsScript.settings.soundVolume);
	}
}
