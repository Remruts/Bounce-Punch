using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class charScript : MonoBehaviour {

	//[Range(1, 4)]
	public int playerId = 1;
	public float hurtCooldown = 0.3f;

	// Stats
	[Header("Stats")]
	[Tooltip("Strength")]
	public float str = 1f;
	[Tooltip("Weight")]
	public float weight = 1f;
	[Tooltip("Resistance")]
	public float res = 1f;
	[Tooltip("Base Speed")]
	public float baseSpeed = 5f;
	[Space(10)]

	public GameObject markerPrefab;

	Animator myAnim;
	Rigidbody2D rb;
	SpriteRenderer sprRenderer;
	Vector2 startPos;
	GameObject marker;

	float angle = 0f;
	float minSpeed = 2f;

	float punchReactionTime = 0.15f;
	float timeToPunch = 0f;
	bool tapStick = false;
	float hurtTimer = 0f;

	void Awake(){
		myAnim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		sprRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		startPos = transform.position;

		marker = Instantiate (markerPrefab, 
			new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 0.7f), 
			Quaternion.identity) as GameObject;
		marker.GetComponent<selectSprite> ().changeSprite (playerId - 1);
	}

	void Update () {

		if (!managerScript.manager.isPaused ()) {

			AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);

			if (state.IsName ("moving") || state.IsName ("idle")) {
				idle ();
			}

			if (state.IsName ("hurt")) {
				if (hurtTimer > 0) {
					hurtTimer -= Time.deltaTime;
					if (hurtTimer <= 0) {
						hurtTimer = 0f;
						myAnim.SetBool ("hurt", false);
					}
				}
			}

			// Smash attack timer
			if (timeToPunch > 0f) {
				timeToPunch -= Time.deltaTime;
				if (timeToPunch < 0f) {
					timeToPunch = 0f;
				}
			}

			//FIXME: Poner Blast Zones
			if (Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 9) {
				transform.position = startPos;
				managerScript.manager.resetChar (playerId);
			}

			// Ajustar velocidad
			if (rb.velocity.sqrMagnitude < minSpeed * minSpeed) {
				if (rb.velocity.sqrMagnitude < 0.1) {
					rb.velocity = (Vector3.zero-transform.position).normalized * minSpeed;
				} else {
					rb.velocity = rb.velocity.normalized * minSpeed;
				}
			}
		}

		if (marker != null) {
			marker.transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 0.7f);
		}

	}

	void idle(){
		float cX = Input.GetAxis("j" + playerId + "Horizontal");
		float cY = -Input.GetAxis("j" + playerId + "Vertical");

		if (Mathf.Abs (cX) > 0.05 || Mathf.Abs (cY) > 0.05) {
			angle = Mathf.Atan2 (cY, cX) * Mathf.Rad2Deg;

			if (angle < 0) {
				angle += 360;
			} else if (angle > 360) {
				angle -= 360; // esto no va a pasar nunca...
			}

			if (angle > 90 && angle < 270) {
				sprRenderer.flipY = true;
			} else {
				sprRenderer.flipY = false;
			}

			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			if (!tapStick) {
				timeToPunch = punchReactionTime;
				tapStick = true;
			}
		} else {
			tapStick = false;
		}

		if (Input.GetButtonDown ("j" + playerId + "Attack")) {
			if (timeToPunch > 0) {
				myAnim.SetTrigger ("hvpunch");
			} else {
				myAnim.SetTrigger ("ltpunch");
			}
		} else if (Input.GetButtonDown ("j" + playerId + "Special")) {
			//myAnim.SetTrigger ("hvpunch");
			myAnim.SetBool ("hurt", true);
			hurtTimer = hurtCooldown;
		} else if (Input.GetButtonDown ("j" + playerId + "Block")) {
			myAnim.SetTrigger ("block");
		} else if (Input.GetButtonDown ("j" + playerId + "Evade")) {
			myAnim.SetTrigger ("dodge");
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag ("Paddle")) {

			// EXPERIMENTAL: normalizar velocidad
			if (Input.GetButton("j" + playerId + "CW") && Input.GetButton("j" + playerId + "CCW")){
				rb.velocity = rb.velocity.normalized * baseSpeed;
			}

			if (rb.velocity.sqrMagnitude < (baseSpeed * baseSpeed)) {
				rb.velocity = rb.velocity.normalized * baseSpeed;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Hitbox")) {			
			if (!myAnim.GetBool("hurt") && (other.transform.parent != transform)){
				hitboxScript hs = other.GetComponent<hitboxScript> ();
				if (hs != null) {
					myAnim.SetBool ("hurt", true);
					hurtTimer = hurtCooldown * hs.knockback;
					camScript.screen.shake (0.1f, 0.5f * hs.knockback);

					Transform father = other.transform.parent;
					float otherAngle = father.rotation.eulerAngles.z * Mathf.Deg2Rad;
					Vector2 dir = new Vector2 (Mathf.Cos (otherAngle), Mathf.Sin (otherAngle));

					rb.velocity = dir.normalized * hs.knockback * 10f /weight;

					father.gameObject.GetComponent<Rigidbody2D> ().velocity = -dir.normalized * baseSpeed/2f * hs.knockback;
				}
			}
		}
	}

	// launch at an angle
	public void launch(float ang){
		angle = ang;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		if (angle < 0) {
			angle += 360;
		} else if (angle > 360) {
			angle -= 360; // esto no va a pasar nunca...
		}

		if (angle > 90 && angle < 270) {
			sprRenderer.flipY = true;
		} else {
			sprRenderer.flipY = false;
		}

		float cX = Mathf.Cos (angle * Mathf.Deg2Rad) * baseSpeed;
		float cY = Mathf.Sin (angle * Mathf.Deg2Rad) * baseSpeed;
		rb.velocity =  new Vector2(cX, cY);
	}

	void OnDestroy(){
		if (marker != null) {
			Destroy (marker);
		}
	}

}
