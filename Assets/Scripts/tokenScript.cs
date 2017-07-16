using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenScript : MonoBehaviour {

	public int playerId = 1;
	public SpriteRenderer portrait;
	public bool CPU = false;

	void OnTriggerStay2D(Collider2D other){
		if (transform.parent == null){
			if (other.CompareTag("playerButton")){
				playerButtonScript scr = other.gameObject.GetComponent<playerButtonScript>();			
				portrait.sprite = scr.portrait;
				if (CPU){
					GameObject chr =  scr.AICharacter;
					settingsScript.settings.characters[playerId-1] = chr;
				} else {
					GameObject chr =  scr.character;
					settingsScript.settings.characters[playerId-1] = chr;
				}

			}
		}
	}
}
