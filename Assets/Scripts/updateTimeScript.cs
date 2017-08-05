using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateTimeScript : MonoBehaviour {

	TextMesh t;
	int time = 0;
	// Use this for initialization
	void Start () {
		t = GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update () {
		if (settingsScript.settings.stockBattle){
			time = settingsScript.settings.maxLives;
		} else {
			time = settingsScript.settings.matchTime;
		}
		if (time > 0){
			t.text = time.ToString();
			t.fontSize = 30;
		} else {
			t.text = "∞";
			t.fontSize = 60;
		}

	}
}
