using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour {

	public static inputManager inputman;

	public string[] attackButton;
	public string[] blockButton;
	public string[] specialButton;
	public string[] dodgeButton;
	public string[] startButton;
	public string[] CWButton;
	public string[] CCWButton;
	public string[] CWSlowButton;
	public string[] CCWSlowButton;

	// Use this for initialization
	void Awake () {
		if (inputman == null){
			DontDestroyOnLoad(gameObject);
			inputman = this;
		} else {
			if (inputman != this){
				Destroy(gameObject);
				return;
			}
		}

		attackButton = new string[4];
		blockButton = new string[4];
		specialButton = new string[4];
		dodgeButton = new string[4];
		startButton = new string[4];
		CWButton = new string[4];
		CCWButton = new string[4];
		CWSlowButton = new string[4];
		CCWSlowButton = new string[4];

		for (int i = 0; i < 4; ++i){
			resetButtons(i);
		}
	}

	public void resetButtons(int i){
		attackButton[i] = "joystick " + (i + 1) + " button 2";
		blockButton[i] = "joystick " + (i + 1) + " button 0";
		specialButton[i] = "joystick " + (i + 1) + " button 3";
		dodgeButton[i] = "joystick " + (i + 1) + " button 1";
		startButton[i] = "joystick " + (i + 1) + " button 7";
		CWButton[i] = "joystick " + (i + 1) + " button 5";
		CCWButton[i] = "joystick " + (i + 1) + " button 4";
		CWSlowButton[i] = "joystick " + (i + 1) + " button 9";
		CCWSlowButton[i] = "joystick " + (i + 1) + " button 8";
	}

	public bool Attack(int id){
		return Input.GetKeyDown (attackButton[id]);
	}

	public void setAttack(int id, string button){
		swap(id, button, attackButton[id]);
		attackButton[id] = button;
	}

	public bool Block(int id){
		return Input.GetKeyDown (blockButton[id]);
	}

	public void setBlock(int id, string button){
		swap(id, button, blockButton[id]);
		blockButton[id] = button;
	}

	public bool Dodge(int id){
		return Input.GetKeyDown (dodgeButton[id]);
	}

	public void setDodge(int id, string button){
		swap(id, button, dodgeButton[id]);
		dodgeButton[id] = button;
	}

	public bool Special(int id){
		return Input.GetKeyDown (specialButton[id]);
	}

	public void setSpecial(int id, string button){
		swap(id, button, specialButton[id]);
		specialButton[id] = button;
	}

	public bool StartButton(int id){
		return Input.GetKeyDown (startButton[id]);
	}

	public void setStart(int id, string button){
		swap(id, button, startButton[id]);
		startButton[id] = button;
	}

	public bool CW(int id){
		return Input.GetKey (CWButton[id]);
	}

	public void setCW(int id, string button){
		swap(id, button, CWButton[id]);
		CWButton[id] = button;
	}

	public bool CCW(int id){
		return Input.GetKey (CCWButton[id]);
	}

	public void setCCW(int id, string button){
		swap(id, button, CCWButton[id]);
		CCWButton[id] = button;
	}

	public bool CWSlow(int id){
		return Input.GetKey (CWSlowButton[id]);
	}

	public void setCWSlow(int id, string button){
		swap(id, button, CWSlowButton[id]);
		CWSlowButton[id] = button;
	}

	public bool CCWSlow(int id){
		return Input.GetKey (CCWSlowButton[id]);
	}

	public void setCCWSlow(int id, string button){
		swap(id, button, CCWSlowButton[id]);
		CCWSlowButton[id] = button;
	}

	void swap(int id, string button, string swapString){
		if (attackButton[id] == button){
			attackButton[id] = swapString;
		}
		if (blockButton[id] == button){
			blockButton[id] = swapString;
		}
		if (specialButton[id] == button){
			specialButton[id] = swapString;
		}
		if (dodgeButton[id] == button){
			dodgeButton[id] = swapString;
		}
		if (startButton[id] == button){
			startButton[id] = swapString;
		}
		if (CWButton[id] == button){
			CWButton[id] = swapString;
		}
		if (CCWButton[id] == button){
			CCWButton[id] = swapString;
		}
		if (CWSlowButton[id] == button){
			CWSlowButton[id] = swapString;
		}
		if (CCWSlowButton[id] == button){
			CCWSlowButton[id] = swapString;
		}
	}
}
