using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class managerScript : MonoBehaviour {

	public static managerScript manager;
	public GameObject paddlePrefab;
	public GameObject plus1Prefab;
	public GameObject UIBarPrefab;
	public GameObject Canvas;
	[Space(5)]
	public Text timerText;
	public float resetTime = 1f;

	bool paused = false;
	int pausedPlayer = 0;
	int playerNum;
	float currentTime = 0;

	List<GameObject> activeChars;
	List<GameObject> paddles;
	List<GameObject> UIBars;
	private Text[] pointCounters;
	int[] playerPoints;

	void Start () {
		if (manager == null){
			manager = this;
		} else {
			if (manager != this){
				Destroy(gameObject);
			}
		}

		playerNum = settingsScript.settings.characters.Length;
		playerPoints = new int[playerNum];
		pointCounters = new Text[playerNum];

		currentTime = settingsScript.settings.matchTime;

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
		UIBars = new List<GameObject> ();
		int i = 1;

		foreach(var chara in settingsScript.settings.characters){

			//Barra de UI
			GameObject ui = Instantiate (UIBarPrefab) as GameObject;
			UIBars.Add (ui);
			ui.transform.SetParent(Canvas.transform, false);
			setUIBar (i-1, 10);

			//Ángulo
			radAngle = startAngle * Mathf.Deg2Rad;

			cX = Mathf.Cos(radAngle);
			cY = Mathf.Sin(radAngle);

			// Instancio Personaje
			GameObject c = Instantiate(chara, new Vector3(cX, cY, 0), Quaternion.identity) as GameObject;

			activeChars.Add(c);

			charScript scr = c.GetComponent<charScript>();
			if (scr != null){
				scr.playerId = i;
				scr.launch(startAngle);
			} else {
				print("failed");
			}

			// Instancio paleta
			paddles.Add( Instantiate(paddlePrefab) as GameObject);
			paddleScript pd = paddles[i-1].GetComponent<paddleScript>();
			pd.playerId = i;
			pd.setAngle(startAngle);
			paddles [i - 1].GetComponent<selectSprite> ().changeSprite (i - 1);

			// Próximo
			i++;

			startAngle -= angDiv;
			if (startAngle < 0){
				startAngle += 360;
			}
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
			if (currentTime > 0) {
				currentTime -= Time.deltaTime;
				timerText.text = currentTime.ToString ("F0");
			}
		}
	}

	public bool isPaused(){
		return paused;
	}

	public void resetChar(int id){
		activeChars[id - 1].SetActive (false);
		StartCoroutine (resetCharInTime (id, resetTime));
	}

	public IEnumerator resetCharInTime(int id, float time){
		
		yield return new WaitForSeconds(time);
		activeChars[id - 1].SetActive (true);

		charScript scr = activeChars[id-1].GetComponent<charScript>();
		scr.life = scr.res;

		Vector3 pos = (paddles[id-1].transform.position -
			activeChars[id-1].transform.position).normalized;

		float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		scr.launch(angle);
	}

	public void givePoints(int id){
		playerPoints [id-1] += 1;
		GameObject plus1obj = Instantiate(plus1Prefab, 
			activeChars[id-1].transform.position, 
			Quaternion.identity) as GameObject;

		Vector2 dir = Vector2.up*8;
		dir.x = Random.Range (-5, 5);
		pointCounters [id - 1].text = playerPoints [id - 1].ToString();

		plus1obj.GetComponent<Rigidbody2D> ().velocity = dir;
		plus1obj.GetComponent<selectSprite> ().changeSprite (id - 1);

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
