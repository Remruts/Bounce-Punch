using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMURDspecialScript : specialScript {

	//Animator myAnim;
	charScript myScript;
	public GameObject swish;

	void Start () {
		//myAnim = GetComponent<Animator> ();
		myScript = GetComponent<charScript>();
	}

	override public void special(){
		Instantiate(swish, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));
		camScript.screen.zoom (0.2f, 7f);
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * 10f;
		myScript.specialCharge = 0f;
		Invoke("recharge", 1f);
	}

	void recharge(){
		myScript.specialCharge = 1f;
	}
}
