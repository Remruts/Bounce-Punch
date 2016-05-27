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

		if (managerScript.manager != null) {
			winner = managerScript.manager.getWinner ();
		}

		if (winner > -1) {
			txt.text = "Player " + (winner + 1) + " wins!";
		} else{
			txt.text = "Draw";
		}

		//SceneManager.LoadScene ("gameScene");
	}

}
