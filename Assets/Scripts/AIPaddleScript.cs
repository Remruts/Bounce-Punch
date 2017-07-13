using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddleScript : MonoBehaviour {

	public float radius = 6.5f;
	public float maxSpeed = 7f;
	public float acceleration = 0.7f;

	[Range(0f, 1f)]
	public float drag = 0.13f;

	public GameObject target;
	Rigidbody2D rb;

	float speed = 0f;
	float angle = 0f;
	float x, y;

	void Awake() {
		speed = 0f;
		angle = 0f;

		x = radius;
		y = 0f;
	}

	void Start(){
		rb = target.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		if (!managerScript.manager.isPaused ()) {
			angle += speed;

			if (angle < 0f)
				angle += 360f;
			if (angle >= 360f)
				angle -= 360f;

			speed *= (1 - drag);

			float angle2Rad = angle * Mathf.Deg2Rad;
			x = Mathf.Cos(angle2Rad) * radius;
			y = Mathf.Sin(angle2Rad) * radius;

			float targetAngle = predictAngle();

			if (targetAngle < 0f)
				targetAngle += 2*Mathf.PI;
			if (targetAngle >= 2*Mathf.PI)
				targetAngle -= 2*Mathf.PI;

			if (Mathf.Abs(angle2Rad - targetAngle) > 0.1){
				if (angle2Rad < targetAngle){
					if ((angle2Rad + 2*Mathf.PI - targetAngle) > targetAngle - angle2Rad){
						// CCW
						if (speed < maxSpeed)
							speed += acceleration/2;
					} else {
						// CW
						if (speed > -maxSpeed)
							speed -= acceleration/2;
					}
				} else {
					if ((targetAngle + 2*Mathf.PI - angle2Rad) > angle2Rad - targetAngle){
						// CW
						if (speed > -maxSpeed)
							speed -= acceleration/2;
					} else {
						// CCW
						if (speed < maxSpeed)
							speed += acceleration/2;
					}
				}
			}

			transform.position = new Vector2 (x, y);
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}

	public void setAngle(float a){
		angle = a;
	}

	float predictAngle(){
		Vector2 segment = rb.velocity.normalized * - 10f;
		if (segment.magnitude == 0){
			segment.x = 1f;
		}

		Vector2 pos = target.transform.position;
		float x0 = pos.x;
		float y0 = pos.y;
		float x1 = segment.x;
		float y1 = segment.y;

		float a = (x1 - x0)*(x1 - x0) + (y1 - y0)*(y1 - y0);
		float b = 2*(x1 - x0)*x0 + 2*(y1 - y0)*y0;
		float c = x0*x0 + y0*y0 - 36;

		float t = 2*c/(-b + Mathf.Sqrt(b*b - 4 * a*c));

		//Vector3 newPos = new Vector3((x1 - x0) * t + x0, (y1 - y0) * t + y0, 0);
		//Debug.DrawLine(pos, newPos, Color.red);

		return Mathf.Atan2((y1 - y0) * t + y0, (x1 - x0) * t + x0);
	}
}
