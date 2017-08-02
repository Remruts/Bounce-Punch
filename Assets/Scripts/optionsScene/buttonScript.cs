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
			spr.color = selectedColor;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		spr.color = Color.white;
	}

	void OnMouseEnter(){
		audioSource.PlayOneShot(selectSound, settingsScript.settings.soundVolume);
	}

	void OnMouseOver(){
		spr.color = selectedColor;
	}

	void OnMouseExit(){
		spr.color = Color.white;
	}

	void OnMouseDown(){
		press();
	}
}
