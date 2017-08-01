using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mummycatSpecialScript : specialScript {

	Animator myAnim;
	public GameObject sparks;
	public GameObject beam;

	public AudioClip beamSound;
	AudioSource audioSource;

	GameObject laser;

	void Start () {
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	void Update(){
		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (!state.IsName ("special")){
			if (laser != null){
				Destroy(laser);
			}
		}

	}

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		myAnim.SetTrigger ("special");

		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

		Invoke("shootLasers", 0.1f);
	}

	void shootLasers(){

		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("special") && gameObject.activeSelf){
			laser = Instantiate (beam, transform.position + transform.right * 1f, transform.rotation) as GameObject;
			laser.GetComponent<beamGeneratorScript>().hitter = gameObject;
			//laser.transform.parent = transform;
			audioSource.PlayOneShot(beamSound, settingsScript.settings.soundVolume);
		}
	}
}
