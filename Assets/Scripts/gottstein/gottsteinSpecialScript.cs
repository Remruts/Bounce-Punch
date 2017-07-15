using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gottsteinSpecialScript : specialScript {

	public GameObject wall;

	override public void special(){

		camScript.screen.zoom (0.2f, 7f);
		Instantiate (wall, transform.position + transform.right, transform.rotation);

	}
}
