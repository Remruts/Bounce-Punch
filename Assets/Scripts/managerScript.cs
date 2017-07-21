using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class managerScript : MonoBehaviour {

	public static managerScript manager;
	public GameObject paddlePrefab;
	public GameObject AIPaddlePrefab;
	public GameObject plus1Prefab;
	public GameObject UIBarPrefab;
	public GameObject Canvas;
	[Space(5)]
	public Text timerText;
	public float respawnTime = 1f;
	public float transitionTime = 6f;
	[Space(5)]
	public GameObject greatCircle;

	bool playing = true;
	bool paused = false;
	int pausedPlayer = 0;
	int playerNum;
	float currentTime = 0;

	GameObject[] activeChars;
	GameObject[] paddles;
	GameObject[] UIBars;
	Text[] pointCounters;
	int[] playerPoints;

	void Start () {
		if (manager == null){
			manager = this;
		} else {
			if (manager != this){
				Destroy(gameObject);
			}
		}

		playerNum = settingsScript.settings.getPlayerNumber();
		playerPoints = new int[4];
		pointCounters = new Text[4];

		currentTime = settingsScript.settings.matchTime;

		float angDiv = 360.0f/(float)playerNum;
		float startAngle;

		float camSize = 7f;
		float paddleRad = 6.5f;

		switch (playerNum){
		case 4:
			camSize = 6.5f;
			startAngle = 135;
		break;
		case 3:
			camSize = 6f;
			startAngle = 150;
		break;
		default:
			camSize = 5.5f;
			startAngle = 180;
		break;
		}

		Camera.main.orthographicSize = camSize;
		greatCircle.transform.localScale = new Vector2 (camSize / 7f, camSize / 7f);
		paddleRad = camSize - 0.5f;

		float cX, cY;
		float radAngle;

		activeChars = new GameObject[4];
		paddles = new GameObject[4];
		UIBars = new GameObject[4];

		int i = 0;
		for (int j = 0; j < 4; j++){

			GameObject chara = settingsScript.settings.characters[j];

			//Barra de UI
			GameObject ui = Instantiate (UIBarPrefab) as GameObject;
			UIBars[j] = ui;
			ui.transform.SetParent(Canvas.transform, false);
			setUIBar (j, 10);

			if (chara == null){
				ui.SetActive(false);
				continue;
			}

			//Ángulo
			radAngle = startAngle * Mathf.Deg2Rad;

			cX = Mathf.Cos(radAngle);
			cY = Mathf.Sin(radAngle);

			// Instancio Personaje
			GameObject c = Instantiate(chara, new Vector3(cX, cY, 0), Quaternion.identity) as GameObject;

			activeChars[j] = c;

			charScript scr = c.GetComponent<charScript>();
			if (scr != null){
				scr.playerId = j+1;
				scr.launch(startAngle);
				//resetChar (i);
			} else {
				print("failed");
			}

			// Instancio paleta
			if (scr.CPU){
				paddles[j] = Instantiate(AIPaddlePrefab) as GameObject;
				AIPaddleScript pd = paddles[j].GetComponent<AIPaddleScript>();
				pd.target = c;
				pd.setAngle(startAngle);
				pd.radius = paddleRad;
				paddles[j].GetComponent<selectSprite>().changeSprite(j);
			} else {
				paddles[j] = Instantiate(paddlePrefab) as GameObject;
				paddleScript pd = paddles[j].GetComponent<paddleScript>();
				pd.setAngle(startAngle);
				pd.radius = paddleRad;
				paddles[j].GetComponent<selectSprite>().changeSprite(j);
			}
			paddleSettingsScript pss = paddles[j].GetComponent<paddleSettingsScript>();
			pss.playerId = j+1;

			startAngle -= angDiv;
			if (startAngle < 0){
				startAngle += 360;
			}
			i++;
		}
	}

	// Función para posicionar cada Barra de UI en su lugar
	void setUIBar(int num, float yPos){

		float anchorX = (num % 3 > 0) ? 1 : 0;
		float anchorY = (num < 2) ? 1 : 0;
		float scale = (num % 3 > 0) ? -1 : 1;
		yPos *= (2 * (-anchorY) + 1);

		RectTransform rect = UIBars [num].GetComponent<Image> ().rectTransform;
		rect.localScale = new Vector2 (scale, 1);
		rect.anchorMax = new Vector2 (anchorX, anchorY);
		rect.anchorMin = rect.anchorMax;
		rect.pivot = new Vector2(0, anchorY);
		rect.anchoredPosition = new Vector2 (0, yPos);

		UIBars[num].GetComponent<UIBarScript> ().
			setPlayerIndicator (num, 1 - anchorY, scale, -8 -16 * anchorY);

		pointCounters [num] = UIBars [num].GetComponentInChildren<Text> ();

		if (num % 3 > 0) {
			pointCounters [num].transform.localScale = new Vector2 (-1, 1);
			pointCounters [num].alignment = TextAnchor.MiddleRight;
			RectTransform rect2 = pointCounters [num].gameObject.GetComponent<RectTransform> ();
			rect2.pivot = new Vector2 (1, 0.5f);
		}
	}

	public UIBarScript getUIBar(int id){
		return UIBars[id-1].GetComponent<UIBarScript> ();
	}

	public void specialCharge(int id, bool max){
		id -= 1; //normalizar [0, 3]

		UIBarScript barscr = UIBars [id].GetComponent<UIBarScript> ();
		barscr.buttonToggle (max);
	}

	public void specialReset(int id){
		id -= 1;
		UIBarScript barscr = UIBars [id].GetComponent<UIBarScript> ();
		barscr.specialReset ();
	}

	// Update is called once per frame
	void Update () {
		if (playing) {
			// check pause
			if (paused) {
				if (Input.GetButtonDown ("j" + (pausedPlayer + 1) + "Start")) {
					paused = false;
					Time.timeScale = 1f;
				}
				timerText.text = "BOUNCE\nPUNCH!";
			} else {
				for (int i = 0; i < playerNum; ++i) {
					if (Input.GetButtonDown ("j" + (i + 1) + "Start")) {
						paused = true;
						pausedPlayer = i;
						Time.timeScale = 0f;
						break;
					}
				}

				// update timer
				if (currentTime > 0) {
					currentTime -= Time.deltaTime;
					timerText.text = currentTime.ToString ("F0");
				} else {
					//if match ended
					if (currentTime < 0) {
						currentTime = 0;
					}

					timerText.text = "0";
					playing = false;
					Time.timeScale = 0.1f;
					StartCoroutine (endGame (transitionTime * 0.1f));
				}
			}
		}
	}

	public bool checkIfPlaying(){
		return playing;
	}

	public bool isPaused(){
		return paused;
	}

	public void resetChar(int id){
		activeChars[id - 1].SetActive (false);
		StartCoroutine (resetCharInTime (id, respawnTime));
	}

	IEnumerator endGame(float time){
		yield return new WaitForSeconds(time/2f);
		transitionScript.transition.startTransition (transitionTime/2f);
		yield return new WaitForSeconds(time/2f);
		Time.timeScale = 1f;
		pickWinner ();
		SceneManager.LoadScene ("resultsScene");
	}

	IEnumerator resetCharInTime(int id, float time){
		yield return new WaitForSeconds(time);
		activeChars[id - 1].SetActive (true);

		charScript scr = activeChars[id-1].GetComponent<charScript>();
		scr.life = scr.res;
		scr.invensibilityTimer = 1.5f;

		Vector3 pos = (paddles[id-1].transform.position -
			activeChars[id-1].transform.position).normalized;

		float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		scr.launch(angle);
	}

	public void pickWinner(){
		int winner = -1;
		int winnerPoints = 0;

		for (int i = 0; i < playerNum; ++i) {
			if (playerPoints [i] > winnerPoints) {
				winner = i;
				winnerPoints = playerPoints [i];
			} else if (playerPoints [i] == winnerPoints){
				winner = -1;
			}
		}

		settingsScript.settings.setWinner (winner);
	}

	public void givePoints(int id, int points){
		if (!playing) {
			return;
		}

		playerPoints [id-1] += points;
		if (playerPoints [id - 1] < 0) {
			playerPoints [id - 1] = 0;
		}
		pointCounters [id - 1].text = playerPoints [id - 1].ToString();

		if (points > 0) {
			GameObject plus1obj = Instantiate(plus1Prefab,
				activeChars[id-1].transform.position,
				Quaternion.identity) as GameObject;

			Vector2 dir = Vector2.up*8;
			dir.x = Random.Range (-5, 5);

			plus1obj.GetComponent<Rigidbody2D> ().velocity = dir;
			plus1obj.GetComponent<selectSprite> ().changeSprite (id - 1);
		}

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
