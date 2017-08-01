using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMURDltpunchScript : lightPunchScript {

	Animator myAnim;
	public GameObject laser;
	public float laserSpeed = 15.0f;

	public AudioClip laserSound;
	AudioSource audioSource;

	void Start () {
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	override public void ltPunch(){

		myAnim.SetTrigger ("ltpunch");
		Invoke("shootLaser", 0.05f);
	}

	void shootLaser(){
		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("ltPunch") && gameObject.activeSelf){
			GameObject projectile = Instantiate (laser, transform.position + transform.right * 0.5f, transform.rotation) as GameObject;
			projectile.GetComponent<hitboxScript>().hitter = gameObject;
			projectile.GetComponent<Rigidbody2D>().velocity = transform.right * laserSpeed;

			audioSource.PlayOneShot(laserSound, settingsScript.settings.soundVolume);
		}
	}



}
