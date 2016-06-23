using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class winTextScript : MonoBehaviour {

	Text txt;
	int winner = -1;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();

		if (settingsScript.settings != null) {
			winner = settingsScript.settings.getWinner ();
		}

		if (winner > -1) {
			txt.text = "Player " + (winner + 1) + " wins!";
		} else{
			txt.text = "Draw";
		}

		transitionScript.transition.startTransition (1.7f);
	}

	void Update(){
		if (Input.GetButtonDown ("j1Start")){
			SceneManager.LoadScene ("gameScene");
		}
	}

}
