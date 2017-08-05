using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {

	protected AudioSource audioSource;
	public AudioClip selectSound;
	public AudioClip pressSound;
	public Color selectedColor = Color.black;

	protected SpriteRenderer spr;

	void Start(){
		spr = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	public virtual void press(){

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("hand")){
			audioSource.PlayOneShot(selectSound, settingsScript.settings.soundVolume);
			if (spr != null){
				spr.color = selectedColor;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (spr != null){
			spr.color = Color.white;
		}
	}

	void OnMouseEnter(){
		audioSource.PlayOneShot(selectSound, settingsScript.settings.soundVolume);
	}

	void OnMouseOver(){
		if (spr != null){
			spr.color = selectedColor;
		}
	}

	void OnMouseExit(){
		if (spr != null){
			spr.color = Color.white;
		}
	}

	void OnMouseDown(){
		press();
	}
}
