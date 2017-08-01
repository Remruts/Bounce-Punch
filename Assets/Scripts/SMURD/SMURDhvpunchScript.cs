using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMURDhvpunchScript : heavyPunchScript {

	Animator myAnim;
	public GameObject missile;
	public float missileSpeed = 10.0f;

	public AudioClip missileSound;
	AudioSource audioSource;

	void Start () {
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	override public void hvPunch(){

		myAnim.SetTrigger ("hvpunch");
		Invoke("shootMissile", 0.5f);
	}

	void shootMissile(){
		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("hvPunch")  && gameObject.activeSelf){
			GameObject projectile = Instantiate (missile, transform.position + transform.right * 1.0f, transform.rotation) as GameObject;
			projectile.GetComponent<hitboxScript>().hitter = gameObject;
			projectile.GetComponent<Rigidbody2D>().velocity = transform.right * missileSpeed;

			audioSource.PlayOneShot(missileSound, settingsScript.settings.soundVolume);
		}
	}



}
