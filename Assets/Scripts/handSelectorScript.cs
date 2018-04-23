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

		float cX = 0;
		float cY = 0;
		if (inputManager.inputman.keyboard[playerId-1]){
			cX = inputManager.inputman.Right(playerId-1) ? 0.2f : 0;
			cX -= inputManager.inputman.Left(playerId-1) ? 0.2f : 0;
			cY = inputManager.inputman.Up(playerId-1) ? 0.2f : 0;
			cY -= inputManager.inputman.Down(playerId-1) ? 0.2f : 0;
		} else {
			cX = Input.GetAxis("j" + playerId + "Horizontal");
			cY = -Input.GetAxis("j" + playerId + "Vertical");
		}

		if (Mathf.Abs (cX) > 0.05 || Mathf.Abs (cY) > 0.05) {
			Vector3 newPos = transform.position + new Vector3(cX, cY, 0) * 1.5f;
			newPos.x = Mathf.Clamp(newPos.x, -screenPos.x, screenPos.x);
			newPos.y = Mathf.Clamp(newPos.y, -screenPos.y, screenPos.y);
			transform.position = newPos;
		}

		if (inputManager.inputman.Attack(playerId-1)) {
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
					hitColliders[i].gameObject.GetComponent<buttonScript>().press();
					break;
				}else if (hitColliders[i].tag.Equals("startButton")){
					if (settingsScript.settings.getPlayerNumber() >= 2){
						hitColliders[i].gameObject.GetComponent<buttonScript>().press();
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
				}else if (hitColliders[i].tag.Equals("bigButton")){
					if (!tokenTaken){
						bigButtonScript scr =  hitColliders[i].gameObject.GetComponent<bigButtonScript>();
						scr.toggle();
					}
					break;
				}  else if (hitColliders[i].tag.Equals("playerCard")){
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
				} else if (hitColliders[i].tag.Equals("button")){
					hitColliders[i].gameObject.GetComponent<buttonScript>().press();
					break;
				}
			}
		}
		if (inputManager.inputman.AttackUp(playerId-1)) {
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
