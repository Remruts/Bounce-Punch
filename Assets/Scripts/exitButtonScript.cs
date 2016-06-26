using UnityEngine;
using System.Collections;

public class exitButtonScript : MonoBehaviour {

	public void onClick(){
		Debug.Log ("Exiting game...");
		Application.Quit();
	}
}
