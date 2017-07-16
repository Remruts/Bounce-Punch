using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handSelectorScript : MonoBehaviour {

	public int playerId = 1;
	Animator myAnim;
	bool tokenTaken = true;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myAnim.SetBool("close", true);
	}

	// Update is called once per frame
	void Update () {
		float cX = Input.GetAxis("j" + playerId + "Horizontal");
		float cY = -Input.GetAxis("j" + playerId + "Vertical");
		if (Mathf.Abs (cX) > 0.05 || Mathf.Abs (cY) > 0.05) {
			Vector3 newPos = transform.position + new Vector3(cX, cY, 0) * 1.5f;
			newPos.x = Mathf.Clamp(newPos.x, -10f, 10f);
			newPos.y = Mathf.Clamp(newPos.y, -5f, 5f);
			transform.position = newPos;
		}

		if (Input.GetButtonDown ("j" + playerId + "Attack")) {
			if (!myAnim.GetBool("close")){
				myAnim.SetBool("close", true);
			}
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
			for (int i = 0; i < hitColliders.Length; ++i){
				if (hitColliders[i].tag.Equals("token")){
					tokenScript tkScr = hitColliders[i].gameObject.GetComponent<tokenScript>();
					if (tkScr.playerId == playerId){
						if (tokenTaken){
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
					transitionScript.transition.level = "gameScene";
					transitionScript.transition.startTransition(2f);
					break;
				} else if (hitColliders[i].tag.Equals("playerCard")){
					if (!tokenTaken){
						hitColliders[i].gameObject.GetComponent<playerCardScript>().toggle();
						break;
					}
				} else if (hitColliders[i].tag.Equals("cputoken")){
					if (tokenTaken){
						hitColliders[i].transform.parent = null;
						tokenTaken = false;
					} else {
						hitColliders[i].transform.parent = transform;
						hitColliders[i].transform.localPosition = new Vector3(-0.4f, 0.4f, 0f);
						tokenTaken = true;
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
}
