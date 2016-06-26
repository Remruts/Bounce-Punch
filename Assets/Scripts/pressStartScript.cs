using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pressStartScript : MonoBehaviour {

	public Animator titleAnim;
	public GameObject menuButtons;

	bouncySpriteScript scr;
	bool active = true;
	Text txt;
	RectTransform img;
	RectTransform menu;
	Vector2 menuStartPos;
	float rate;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text>();
		scr = GetComponent<bouncySpriteScript>();
		img = GetComponent<RectTransform>();
		menuButtons.SetActive(false);
	}


	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown){
			active = false;
			scr.enabled = false;
			titleAnim.SetTrigger("pressStart");

			menuButtons.SetActive(true);
			menu = menuButtons.GetComponent<RectTransform>();
			menuStartPos = menu.anchoredPosition;
		}

		if (!active){

			rate = 2f * Time.deltaTime;
			img.localScale += new Vector3(rate*3, -rate);
			Color matColor = txt.color;

			matColor.a -= 2f * rate;
			txt.color = matColor;

			if (menu.anchoredPosition.x > 0){
				menu.anchoredPosition = new Vector2(Mathf.Lerp(0, menuStartPos.x, txt.color.a), menuStartPos.y);
			}

			if (txt.color.a <= 0){
				enabled = false;
			}
		}
	}
}
