using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetButtonScript : MonoBehaviour {

	SpriteRenderer spr;
	void Start(){
		spr = GetComponent<SpriteRenderer>();
	}

	int id = 1;
	void OnMouseDown(){
		press();
	}

	public void press(){
		id = transform.parent.gameObject.GetComponent<buttonsIdScript>().id;
		inputManager.inputman.resetButtons(id - 1);
		spr.color = Color.black;
		Invoke("reset", 0.2f);
	}

	void reset(){
		spr.color = Color.white;
	}

	void OnMouseUp(){
		spr.color = Color.white;
	}
	void OnMouseExit(){
		spr.color = Color.white;
	}
}
