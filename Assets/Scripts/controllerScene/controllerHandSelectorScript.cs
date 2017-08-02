using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerHandSelectorScript : MonoBehaviour {

	public int playerId = 1;
	public TextMesh playerText;
	public GameObject buttons;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		Vector3 pos = new Vector3(screenWidth-32, screenHeight, 0);
		Vector3 screenPos = Camera.main.ScreenToWorldPoint(pos);

		for (int j=1; j<5; ++j){
			float cX = Input.GetAxis("j"+j+"Horizontal");
			float cY = -Input.GetAxis("j"+j+"Vertical");
			if (Mathf.Abs (cX) > 0.05 || Mathf.Abs (cY) > 0.05) {
				Vector3 newPos = transform.position + new Vector3(cX, cY, 0) * 1.5f;
				newPos.x = Mathf.Clamp(newPos.x, -screenPos.x, screenPos.x);
				newPos.y = Mathf.Clamp(newPos.y, -screenPos.y, screenPos.y);
				transform.position = newPos;
			}

			if (Input.GetButtonDown ("j"+j+"Attack")) {
				Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position +transform.up * 0.2f, 0.2f);
				for (int i = 0; i < hitColliders.Length; ++i){
					if (hitColliders[i].tag.Equals("backButton")){
						hitColliders[i].gameObject.GetComponent<buttonScript>().press();
						break;
					} else if (hitColliders[i].tag.Equals("controllerButton")){
						controllerButtonScript scr = hitColliders[i].gameObject.GetComponent<controllerButtonScript>();
						scr.press();
						break;
					} else if (hitColliders[i].tag.Equals("resetButton")){
						hitColliders[i].gameObject.GetComponent<resetButtonScript>().press();
						break;
					} else if (hitColliders[i].tag.Equals("arrowRight")){
						hitColliders[i].gameObject.GetComponent<playerArrowScript>().press();
						break;
					}
					else if (hitColliders[i].tag.Equals("arrowLeft")){
						hitColliders[i].gameObject.GetComponent<playerArrowScript>().press();
						break;
					}
				}
			}
		}
	}

}
