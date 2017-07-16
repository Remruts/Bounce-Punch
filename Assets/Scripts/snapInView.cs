using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapInView : MonoBehaviour {

	public enum Pivot{TopRight, TopLeft, BottomRight, BottomLeft};
	public Pivot pivot;
	public Vector2 position;

	void Update () {
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		if (pivot == Pivot.BottomLeft || pivot == Pivot.BottomRight){
			screenHeight = 0;
		}
		if (pivot == Pivot.BottomLeft || pivot == Pivot.TopLeft){
			screenWidth = 0;
		}

		Vector3 pos = new Vector3(screenWidth + position.x, screenHeight + position.y, 0);
		Vector3 newPos = Camera.main.ScreenToWorldPoint(pos);
		newPos.z = 0;
    	transform.position = newPos;
	}

}
