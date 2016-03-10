using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class selectSprite : MonoBehaviour {

	public Sprite[] sprites;

	SpriteRenderer sprRenderer;

	void Awake () {
		sprRenderer = GetComponent<SpriteRenderer> ();
	}

	public void changeSprite(int i){
		if (i >= sprites.Length) {
			i = sprites.Length - 1;
		} else if (i < 0){
			i = 0;
		}

		sprRenderer.sprite = sprites [i];
	}

}
