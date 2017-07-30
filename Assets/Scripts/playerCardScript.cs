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

	public Sprite RibbonPortrait;
	public GameObject ribbonAI;
	public GameObject ribbon;

	public TextMesh playerText;

	public GameObject manualButton;

	Sprite myCard;
	SpriteRenderer sprRnd;
	void Start(){
		sprRnd = GetComponent<SpriteRenderer>();
		myCard = sprRnd.sprite;
		CPUToken.SetActive(false);
	}
	public void toggle(){
		handSelector.GetComponent<handSelectorScript>().releaseToken();
		if (sprRnd.sprite == myCard){
			settingsScript.settings.characters[playerId-1] = ribbonAI;
			portrait.sprite = RibbonPortrait;

			sprRnd.sprite = CPUCard;
			myToken.SetActive(false);
			CPUToken.SetActive(true);
			playerText.text = "Ribbon";
			manualButton.SetActive(false);

		} else if (sprRnd.sprite == CPUCard){
			settingsScript.settings.characters[playerId-1] = null;
			portrait.sprite = null;
			sprRnd.sprite = ClosedCard;
			myToken.SetActive(false);
			CPUToken.SetActive(false);
			playerText.text = "";
		} else {
			settingsScript.settings.characters[playerId-1] = ribbon;
			portrait.sprite = RibbonPortrait;

			sprRnd.sprite = myCard;
			myToken.SetActive(true);
			CPUToken.SetActive(false);
			playerText.text = "Ribbon";
			manualButton.SetActive(true);
		}
	}
}
