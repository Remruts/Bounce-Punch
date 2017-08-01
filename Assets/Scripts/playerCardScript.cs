using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCardScript : MonoBehaviour {

	public int playerId;
	public Sprite CPUCard;
	public Sprite ClosedCard;

	public GameObject myToken;
	public GameObject CPUToken;
	public GameObject handSelector;
	public SpriteRenderer portrait;

	public TextMesh playerText;

	public GameObject manualButton;

	public bool random = false;

	public Sprite randomPortrait;

	Sprite myCard;
	SpriteRenderer sprRnd;
	void Start(){
		sprRnd = GetComponent<SpriteRenderer>();
		myCard = sprRnd.sprite;
		CPUToken.SetActive(false);

		GameObject chr = settingsScript.settings.characters[playerId-1];
		if (chr != null){
			charScript scr = chr.GetComponent<charScript>();
			if (scr.CPU){
				handSelector.GetComponent<handSelectorScript>().releaseToken();
				sprRnd.sprite = CPUCard;
				CPUToken.transform.position = myToken.transform.position;
				myToken.SetActive(false);
				CPUToken.SetActive(true);
				manualButton.SetActive(false);
			}
		}
	}

	void Update(){
		GameObject chr = settingsScript.settings.characters[playerId-1];
		if (random){
			portrait.sprite = randomPortrait;
			playerText.text = "???";
		} else{
			if (chr != null){
				charScript scr = chr.GetComponent<charScript>();
				playerText.text = scr.charName;
				portrait.sprite = scr.portrait;

			} else {
				portrait.sprite = null;
				playerText.text = "";
			}
		}


	}
	public void toggle(){
		handSelector.GetComponent<handSelectorScript>().releaseToken();
		if (sprRnd.sprite == myCard){
			settingsScript.settings.characters[playerId-1] = null;
			playerText.text = "";

			sprRnd.sprite = CPUCard;
			CPUToken.transform.position = myToken.transform.position;
			myToken.SetActive(false);
			CPUToken.SetActive(true);
			manualButton.SetActive(false);

		} else if (sprRnd.sprite == CPUCard){
			settingsScript.settings.characters[playerId-1] = null;
			portrait.sprite = null;
			sprRnd.sprite = ClosedCard;
			myToken.SetActive(false);
			CPUToken.SetActive(false);
			playerText.text = "";
		} else {
			settingsScript.settings.characters[playerId-1] = null;
			playerText.text = "";

			sprRnd.sprite = myCard;
			myToken.transform.position = CPUToken.transform.position;
			myToken.SetActive(true);
			CPUToken.SetActive(false);
			manualButton.SetActive(true);
		}
	}
}
