using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class eventSystemManager : MonoBehaviour {
	private GameObject currentButton;
	private AxisEventData currentAxis;
	//timer
	private float timeBetweenInputs = 0.3f; //in seconds
	private float timer = 0;

	void Update(){
		if(timer == 0) {
			currentAxis = new AxisEventData (EventSystem.current);
			currentButton = EventSystem.current.currentSelectedGameObject;

			if (Input.GetAxis("j1Vertical") < -0.01) {
				// move up
				currentAxis.moveDir = MoveDirection.Up;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			} else if (Input.GetAxis("j1Vertical") > 0.01) {
				// move down
				currentAxis.moveDir = MoveDirection.Down;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			} else if (Input.GetAxis("j1Horizontal") > 0.01) {
				// move right
				currentAxis.moveDir = MoveDirection.Right;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			} else if (Input.GetAxis("j1Horizontal") < -0.01) {
				// move left
				currentAxis.moveDir = MoveDirection.Left;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			}
		}

         //timer counting down
        if(timer > 0){
			 timer -= Time.fixedDeltaTime;
			 if (timer < 0){
				 timer = 0;
			 }
		}
     }
 }
