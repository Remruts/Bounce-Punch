using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class managerScript : MonoBehaviour {

	public static managerScript manager;
	public GameObject paddlePrefab;

	bool paused = false;
	int pausedPlayer = 0;
	int playerNum;

	List<GameObject> activeChars;
	List<GameObject> paddles;

	// Use this for initialization
	void Start () {
		if (manager == null){
			manager = this;
		} else {
			if (manager != this){
				Destroy(gameObject);
			}
		}

		playerNum = settingsScript.settings.characters.Length;

		float angDiv = 360.0f/(float)playerNum;
		float startAngle;

		switch (playerNum){
		case 4:
			startAngle = 135;
		break;
		case 3:
			startAngle = 150;
		break;
		default:
			startAngle = 180;
		break;
		}

		float cX, cY;
		float radAngle;

		activeChars = new List<GameObject>();
		paddles = new List<GameObject>();
		int i = 1;

		foreach(var chara in settingsScript.settings.characters){

			radAngle = startAngle * Mathf.Deg2Rad;

			cX = Mathf.Cos(radAngle);
			cY = Mathf.Sin(radAngle);

			GameObject c = Instantiate(chara, new Vector3(cX, cY, 0), Quaternion.identity) as GameObject;

			activeChars.Add(c);

			charScript scr = c.GetComponent<charScript>();
			if (scr != null){
				scr.playerId = i;
				scr.launch(startAngle);
			} else {
				print("failed");
			}

			paddles.Add( Instantiate(paddlePrefab) as GameObject);
			paddleScript pd = paddles[i-1].GetComponent<paddleScript>();
			pd.playerId = i;
			pd.setAngle(startAngle);
			paddles [i - 1].GetComponent<selectSprite> ().changeSprite (i - 1);

			i++;

			startAngle -= angDiv;
			if (startAngle < 0){
				startAngle += 360;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		// check pause
		if (paused) {
			if (Input.GetButtonDown ("j" + (pausedPlayer + 1) + "Start")) {
				paused = false;
				Time.timeScale = 1f;
			}
		} else {
			for (int i = 0; i < playerNum; ++i){
				if (Input.GetButtonDown("j" + (i+1) +"Start")){
					paused = true;
					pausedPlayer = i;
					Time.timeScale = 0f;
					break;
				}
			}
		}
	}

	public bool isPaused(){
		return paused;
	}

	public void resetChar(int id){
		charScript scr = activeChars[id-1].GetComponent<charScript>();

		Vector3 pos = (paddles[id-1].transform.position -
			Vector3.zero).normalized;

		float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		scr.launch(angle);
	}

	void OnDestroy(){
		// clean-up
		foreach (var obj in activeChars){
			if (obj != null){
				Destroy(obj);
			}
		}
		foreach (var pad in paddles) {
			if (pad != null) {
				Destroy (pad);
			}
		}

	}
}
