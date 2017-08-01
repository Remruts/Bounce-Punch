using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonspecialScript : specialScript {

	Animator myAnim;
	public GameObject sparks;
	public AudioClip swishsound;
	AudioSource audioSource;

	void Start () {
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		myAnim.SetTrigger ("special");

		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

		audioSource.PlayOneShot(swishsound, settingsScript.settings.soundVolume);

	}
}
