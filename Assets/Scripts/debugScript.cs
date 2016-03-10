using UnityEngine;
using System.Collections;

public class debugScript : MonoBehaviour {

	void Update(){
		if (Input.GetKey("escape")){
			Application.Quit();
		}
	}

	/*
	void OnGUI (){
		GUI.Label(new Rect(10, 10, 100, 30), "maxLives: " + settingsScript.settings.maxLives);
		GUI.Label(new Rect(10, 26, 180, 30), "matchTime: " + settingsScript.settings.matchTime + " min");
		if (GUI.Button(new Rect(10, 44, 100, 16), "SAVE")){
			settingsScript.settings.Save();
		}
		if (GUI.Button(new Rect(10, 62, 100, 16), "LOAD")){
			settingsScript.settings.Load();
		}
		GUI.Label(new Rect(10, 80, 300, 30), Application.persistentDataPath);
	}
	*/

}
