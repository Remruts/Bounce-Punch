using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getKeyScript : MonoBehaviour {

	TextMesh txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update () {
		for (int i = 1; i < 5; ++i){
			for (int j = 0; j < 20; ++j){
				if (Input.GetKeyDown ("joystick " + i +" button " + j)){
					txt.text = "joystick " + i +" button " + j;
				}
			}
		}


	}
}
