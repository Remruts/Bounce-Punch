using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultsScreenScript : MonoBehaviour {

	public GameObject resultsRectangle;
	public GameObject star;
	public string nextScene = "gameScene";

	// Use this for initialization
	void Start () {
		int maxScore = 0;
		for (int i = 0; i < 4; ++i){
			int sc = settingsScript.settings.getScore(i);
			if (sc > maxScore){
				maxScore = sc;
			}
		}
		Vector3 pos = new Vector3(2.25f, 0.5f, 0);
		for (int i = 0; i < 4; ++i){
			if (settingsScript.settings.characters[i] == null)
				continue;
			if (settingsScript.settings.getScore(i) == maxScore){
				Instantiate(star, pos - transform.right * 10f + transform.up *0.1f, Quaternion.identity);
			}
			GameObject rect = Instantiate(resultsRectangle, pos, Quaternion.identity) as GameObject;
			rect.GetComponent<resRectScript>().setPlayer(i);
			pos.y -= 1.75f;
		}
	}

	void Update(){
		if (AudioListener.volume < 1.0f){
			AudioListener.volume += 0.01f;
		}
		for (int i=0; i<4; ++i){
			if (inputManager.inputman.StartButton(i)){
				SceneManager.LoadScene (nextScene);
			}
		}
	}
}
