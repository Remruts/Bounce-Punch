using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tricksterSpecialScript : specialScript {

	Animator myAnim;
	public GameObject sparks;
	public GameObject projectile;

	void Start () {
		myAnim = GetComponent<Animator> ();
	}

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		myAnim.SetTrigger ("special");

		GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

		Invoke("shootLasers", 0.2f);

	}
	void shootLasers(){

		AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("special") && gameObject.activeSelf){
			float angle = 0f;
			for (int i = 0; i < 15; ++i){
				angle = i/15f * 360f;
				Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
				GameObject laser = Instantiate (projectile, transform.position + direction * 1f, Quaternion.identity) as GameObject;

				laser.GetComponent<hitboxScript>().hitter = gameObject;
				laser.GetComponent<Rigidbody2D>().velocity = direction * 5f;
			}

		}
	}
}
