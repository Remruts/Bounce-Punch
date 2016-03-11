using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {

	bool shaking = false;
	float intensity = 0f;
	float shakeIncrement = 1f;

	Vector3 startPos;
	Vector3 shakenPos;

	public static camScript screen;

	void Start(){
		if (screen == null) {
			screen = this;
		} else {
			Destroy (gameObject);
		}
		startPos = transform.position;
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
		}
	}

	public void shake(float time, float factor){

		shaking = true;

		if (time > 0)
			Invoke ("stopShaking", time);

		shakeIncrement = factor;
	}

	public void stopShaking(){
		shaking = false;
	}
}
