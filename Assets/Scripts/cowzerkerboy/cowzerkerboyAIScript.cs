using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowzerkerboyAIScript : idleScript {

	SpriteRenderer sprRenderer;
	charScript scr;

	float angle = 0f;

	// Use this for initialization
	void Start () {
		sprRenderer = GetComponent<SpriteRenderer> ();

		scr = GetComponent<charScript>();
	}

	override public void idle(){
		handleRotation();
		checkHvPunch();
		checkltPunch();
	}

	void checkltPunch(){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
		for (int i = 0; i < hitColliders.Length; ++i){
			if (hitColliders[i].tag.Equals("Player")){
				if (hitColliders[i].gameObject == gameObject){
					continue;
				}
				if (Random.value < 0.1){
					scr.block();
				} else if (Random.value < 0.1){
					scr.dodge();
				}
			}
		}
	}

	void checkHvPunch(){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 3f);
		for (int i = 0; i < hitColliders.Length; ++i){
			if (hitColliders[i].tag.Equals("Player")){
				if (hitColliders[i].gameObject == gameObject){
					continue;
				}
				if (Random.value < 0.1){
					scr.ltPunch();
				} else if (Random.value < 0.05){
					scr.hvPunch();
				} else if (Random.value < 0.05){
					scr.special();
				}
			}
		}
	}

	void handleRotation(){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 6f);
		Transform targetPlayer = null;
		float minDistance = float.MaxValue;

		for (int i = 0; i < hitColliders.Length; ++i){
			if (hitColliders[i].tag.Equals("Player")){
				if (hitColliders[i].gameObject == gameObject){
					continue;
				}
				// Get the position of the collider we are looking at
	            Vector3 possiblePosition = hitColliders[i].transform.position;

	            // Get the distance between us and the collider
	            float currDistance = Vector3.SqrMagnitude(transform.position - possiblePosition);

	            if (currDistance < minDistance) {
	                targetPlayer = hitColliders[i].transform;
	                minDistance = currDistance;
	            }
			}
		}
		if (targetPlayer != null){
			float cX = targetPlayer.position.x;
			float cY = targetPlayer.position.y;

			angle = Mathf.Atan2 (cY, cX) * Mathf.Rad2Deg;

			if (angle < 0) {
				angle += 360;
			} else if (angle > 360) {
				angle -= 360;
			}

			if (angle > 90 && angle < 270) {
				sprRenderer.flipY = true;
			} else {
				sprRenderer.flipY = false;
			}
		}

		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}
}
