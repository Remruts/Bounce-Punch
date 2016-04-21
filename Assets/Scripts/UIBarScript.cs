using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBarScript : MonoBehaviour {

	public Image specialButton;
	public Sprite[] buttonSprites;
	[Space(10)]
	public Image lifeBar;

	bool specialCharged;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (specialCharged) {
			if (specialButton.sprite != buttonSprites [2]) {
				specialButton.sprite = buttonSprites [2];
			}
		}
	}

	public void setLifebar(float amount){
		lifeBar.fillAmount = amount;
	}

	public void buttonToggle (bool max){
		if (max) {
			specialButton.sprite = buttonSprites [2];
			specialCharged = true;
		} else {
			specialButton.sprite = buttonSprites [1];
			StartCoroutine (buttonReset (0.2f));
		}
	}

	public void specialReset(){
		specialCharged = false;
		specialButton.sprite = buttonSprites [0];
	}

	IEnumerator buttonReset(float time){
		yield return new WaitForSeconds (time);
		if (!specialCharged) {
			specialButton.sprite = buttonSprites [0];
		}
	}
}
