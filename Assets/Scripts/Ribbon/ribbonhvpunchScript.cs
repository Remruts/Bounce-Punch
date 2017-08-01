using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbonhvpunchScript : heavyPunchScript {

	Animator myAnim;
	public GameObject sparks;
	public AudioClip swishsound;
	AudioSource audioSource;

	void Start () {
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	override public void hvPunch(){

		myAnim.SetTrigger ("hvpunch");
		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;
		Invoke("playSound", 0.3f);
	}

	void playSound(){
		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("hvPunch")){
			audioSource.PlayOneShot(swishsound, settingsScript.settings.soundVolume);
		}
	}

}
