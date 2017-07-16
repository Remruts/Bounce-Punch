using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCardScript : MonoBehaviour {

	public int playerId;
	public Sprite CPUCard;
	public GameObject myToken;
	public GameObject CPUToken;
	public SpriteRenderer portrait;
	public Sprite RibbonPortrait;
	public GameObject ribbonAI;
	public GameObject ribbon;

	Sprite myCard;
	SpriteRenderer sprRnd;
	void Start(){
		sprRnd = GetComponent<SpriteRenderer>();
		myCard = sprRnd.sprite;
		CPUToken.SetActive(false);
	}
	public void toggle(){
		if (sprRnd.sprite == myCard){
			settingsScript.settings.characters[playerId-1] = ribbonAI;
			portrait.sprite = RibbonPortrait;

			sprRnd.sprite = CPUCard;
			myToken.SetActive(false);
			CPUToken.SetActive(true);
		} else {
			settingsScript.settings.characters[playerId-1] = ribbon;
			portrait.sprite = RibbonPortrait;

			sprRnd.sprite = myCard;
			myToken.SetActive(true);
			CPUToken.SetActive(false);
		}
	}
}
