using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBarScript : MonoBehaviour {

	public Image specialButton;
	public Sprite[] buttonSprites;
	[Space(10)]
	public Image indicator;
	public Sprite[] playerIndicators;
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

	public void setPlayerIndicator(int i, float minmaxY, float scale, float posY){
		indicator.sprite = playerIndicators [i];
		RectTransform rect = indicator.rectTransform;
		rect.localScale = new Vector2 (rect.localScale.x * scale, rect.localScale.y);
		rect.anchorMin = new Vector2 (rect.anchorMin.x, minmaxY);
		rect.anchorMax = rect.anchorMin;
		rect.anchoredPosition = new Vector2 (rect.anchoredPosition.x, posY);
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
