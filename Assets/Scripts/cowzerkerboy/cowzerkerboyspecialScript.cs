using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowzerkerboyspecialScript : specialScript {

	public GameObject fire;
	Animator myAnim;
	charScript myScript;
	GameObject parts;

	void Start () {
		myAnim = GetComponent<Animator> ();
		myScript = GetComponent<charScript>();
	}

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		myAnim.runtimeAnimatorController = Resources.Load("berzerkAnimator") as RuntimeAnimatorController;
		Invoke("returnToSadness", 10f);

		myScript.str = 5f;
		myScript.weight = 5f;
		myScript.res = 1f;
		myScript.baseSpeed = 4f;

		myScript.specialCharge = 0f;

		parts = Instantiate (fire, transform.position, Quaternion.identity) as GameObject;
		parts.transform.parent = transform;

	}

	void returnToSadness(){
		myScript.str = 2f;
		myScript.weight = 1f;
		myScript.res = 3f;
		myScript.baseSpeed = 3f;

		myScript.specialCharge = 0.25f;

		myAnim.runtimeAnimatorController = Resources.Load("cowzerkerboyAnimator") as RuntimeAnimatorController;

		Destroy(parts);
	}
}
