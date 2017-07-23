using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resRectScript : MonoBehaviour {

	public Sprite[] tagSprites;
	public SpriteRenderer playerTag;
	public TextMesh score;
	public TextMesh KO;
	public TextMesh SD;
	public TextMesh deaths;

	public void setPlayer(int i){
		playerTag.sprite = tagSprites[i];
		score.text = settingsScript.settings.getScore(i).ToString();
		KO.text = settingsScript.settings.getKO(i).ToString();
		SD.text = settingsScript.settings.getSD(i).ToString();
		deaths.text = settingsScript.settings.getDeaths(i).ToString();
	}

}
