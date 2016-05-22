using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {

	bool shaking = false;
	float intensity = 0f;
	float shakeIncrement = 1f;

	bool zooming = false;
	float zoomFactor = 0f;
	float zoomIncrement = 0.1f;

	Vector3 startPos;
	Vector3 shakenPos;

	Camera myCam;
	float startSize;

	public static camScript screen;

	void Start(){
		if (screen == null) {
			screen = this;
		} else {
			Destroy (gameObject);
		}
		startPos = transform.position;

		myCam = GetComponent<Camera> ();
		startSize = myCam.orthographicSize;
	}
		
	void Update () {
		if (!managerScript.manager.isPaused ()) {
			if (shaking) {
				intensity += shakeIncrement * Time.deltaTime;
			}

			if (!shaking && intensity > 0) {
				intensity -= shakeIncrement * Time.deltaTime;

				if (intensity < 0) {
					intensity = 0;
				}
			}

			shakenPos.x = startPos.x + Random.Range(-intensity, intensity);
			shakenPos.y = startPos.y + Random.Range(-intensity, intensity);
			shakenPos.z = startPos.z;
			transform.position = shakenPos;

			if (zooming) {
				zoomFactor -= zoomIncrement * Time.deltaTime;
			}

			if (!zooming && zoomFactor < 0) {
				zoomFactor += zoomIncrement * Time.deltaTime;

				if (zoomFactor > 0) {
					zoomFactor = 0;
				}
			}

			myCam.orthographicSize = startSize + zoomFactor;


		}
	}

	public void shake(float time, float factor){

		shaking = true;

		if (time > 0)
			Invoke ("stopShaking", time);

		shakeIncrement = factor;
	}

	public void zoom(float time, float factor){
		zooming = true;
		if (time > 0) {
			Invoke ("stopZooming", time);
		}
		zoomIncrement = factor;
	}

	public void stopShaking(){
		shaking = false;
	}

	public void stopZooming(){
		zooming = false;
	}
}
