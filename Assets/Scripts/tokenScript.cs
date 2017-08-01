using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenScript : MonoBehaviour {

	public int playerId = 1;
	public SpriteRenderer portrait;
	public TextMesh playerText;
	public bool CPU = false;

	public playerCardScript card;

	void OnTriggerStay2D(Collider2D other){
		if (transform.parent == null){
			if (other.CompareTag("playerButton")){

				playerButtonScript scr = other.gameObject.GetComponent<playerButtonScript>();

				card.random = false;
				GameObject chr;
				if (CPU){
					chr =  scr.AICharacter;
				} else {
					chr =  scr.character;
				}
				settingsScript.settings.characters[playerId-1] = chr;

			} else if (other.CompareTag("randomButton")){
				randomButtonScript scr = other.gameObject.GetComponent<randomButtonScript>();
				int seed = scr.getCharSeed();

				card.random = true;

				GameObject chr;
				if (CPU){
					chr = scr.AICharacter[seed];
				} else{
					chr = scr.character[seed];
				}
				settingsScript.settings.characters[playerId-1] = chr;
			}
		}

	}
}
