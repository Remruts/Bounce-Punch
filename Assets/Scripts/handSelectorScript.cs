using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handSelectorScript : MonoBehaviour {

	public int playerId = 1;
	Animator myAnim;
	bool tokenTaken = true;

	// Use this for initialization
	void Awake () {
		myAnim = GetComponent<Animator>();
		myAnim.SetBool("close", true);
	}

	// Update is called once per frame
	void Update () {
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		Vector3 pos = new Vector3(screenWidth-32, screenHeight, 0);
		Vector3 screenPos = Camera.main.ScreenToWorldPoint(pos);

		float cX = Input.GetAxis("j" + playerId + "Horizontal");
		float cY = -Input.GetAxis("j" + playerId + "Vertical");
		if (Mathf.Abs (cX) > 0.05 || Mathf.Abs (cY) > 0.05) {
			Vector3 newPos = transform.position + new Vector3(cX, cY, 0) * 1.5f;
			newPos.x = Mathf.Clamp(newPos.x, -screenPos.x, screenPos.x);
			newPos.y = Mathf.Clamp(newPos.y, -screenPos.y, screenPos.y);
			transform.position = newPos;
		}

		if (Input.GetButtonDown ("j" + playerId + "Attack")) {
			if (!myAnim.GetBool("close")){
				myAnim.SetBool("close", true);
			}
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.2f, 0.2f);
			for (int i = 0; i < hitColliders.Length; ++i){
				if (hitColliders[i].tag.Equals("token")){
					tokenScript tkScr = hitColliders[i].gameObject.GetComponent<tokenScript>();
					if (tkScr.playerId == playerId){
						if (tokenTaken){
							if (hitColliders[i].transform.parent != transform){
								continue;
							}
							hitColliders[i].transform.parent = null;
							tokenTaken = false;
						} else {
							hitColliders[i].transform.parent = transform;
							hitColliders[i].transform.localPosition = new Vector3(-0.4f, 0.4f, 0f);
							tokenTaken = true;
						}
						break;
					}
				} else if (hitColliders[i].tag.Equals("backButton")){
					transitionScript.transition.level = "mainMenuScene";
					transitionScript.transition.startTransition(2f);
					break;
				}else if (hitColliders[i].tag.Equals("startButton")){
					if (settingsScript.settings.getPlayerNumber() >= 2){
						transitionScript.transition.level = "gameScene";
						transitionScript.transition.startTransition(2f);
						break;
					}
				} else if (hitColliders[i].tag.Equals("autoButton")){
					if (!tokenTaken){
						autoButtonScript scr =  hitColliders[i].gameObject.GetComponent<autoButtonScript>();
						if (scr.id == playerId){
							scr.toggle();
						}
					}
					break;
				} else if (hitColliders[i].tag.Equals("playerCard")){
					if (!tokenTaken){
						hitColliders[i].gameObject.GetComponent<playerCardScript>().toggle();
						break;
					}
				} else if (hitColliders[i].tag.Equals("cputoken")){
					if (tokenTaken){
						if (hitColliders[i].transform.parent != transform){
							continue;
						}
						hitColliders[i].transform.parent = null;
						tokenTaken = false;
					} else {
						hitColliders[i].transform.parent = transform;
						hitColliders[i].transform.localPosition = new Vector3(-0.4f, 0.4f, 0f);
						tokenTaken = true;
					}
					break;
				} else if (hitColliders[i].tag.Equals("arrowRight")){
					settingsScript.settings.matchTime += 30;
					if (settingsScript.settings.matchTime > 990){
						settingsScript.settings.matchTime = 30;
					}
					break;
				} else if (hitColliders[i].tag.Equals("arrowLeft")){
					settingsScript.settings.matchTime -= 30;
					if (settingsScript.settings.matchTime < 30){
						settingsScript.settings.matchTime = 990;
					}
					break;
				}
			}
		}
		if (Input.GetButtonUp ("j" + playerId + "Attack")) {
			if (!tokenTaken){
				myAnim.SetBool("close", false);
			}
		}
	}

	public void releaseToken(){
		myAnim.SetBool("close", false);
		tokenTaken = false;
		foreach(Transform child in transform){
			if (child.gameObject.tag.Equals("CPUToken") || child.gameObject.tag.Equals("token")){
				child.parent = null;
			}
		}

	}
}
