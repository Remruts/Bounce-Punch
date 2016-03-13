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
	public GameObject sparks;
	public GameObject crystalDustPrefab;

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
	float invensibilityTimer = 0f;

	int lastHitPlayer = 0;

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

			if (state.IsName ("moving")) {
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

			if (invensibilityTimer > 0) {
				invensibilityTimer -= Time.deltaTime;
				if (invensibilityTimer <= 0) {
					invensibilityTimer = 0;
				}
			}

			// Smash attack timer
			if (timeToPunch > 0f) {
				timeToPunch -= Time.deltaTime;
				if (timeToPunch <= 0f) {
					timeToPunch = 0f;

				}
			}

			// No creo que sean necesarias las blast-zones...
			// death / muerte
			if (Mathf.Abs(transform.position.x) > 14 || Mathf.Abs(transform.position.y) > 8) {

				Instantiate (crystalDustPrefab, transform.position, Quaternion.identity);
				camScript.screen.shake (0.1f, 0.5f);

				transform.position = startPos;

				if (lastHitPlayer > 0) {
					managerScript.manager.givePoints(lastHitPlayer);
					lastHitPlayer = 0;
				}
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
				GameObject parts = Instantiate (sparks, transform.position, Quaternion.identity) as GameObject;
				parts.transform.parent = transform;
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

			if (other.gameObject.GetComponent<paddleScript> ().playerId == playerId) {
				// Sólo si es mi paleta
				// El último que me pegó no es nadie
				lastHitPlayer = 0;
			}

			// EXPERIMENTAL: normalizar velocidad
			if (Input.GetButton("j" + playerId + "CW") && Input.GetButton("j" + playerId + "CCW")){
				rb.velocity = rb.velocity.normalized * baseSpeed;
			}

			float magnitude = rb.velocity.sqrMagnitude;

			if (magnitude < (baseSpeed * baseSpeed)) {
				rb.velocity = rb.velocity.normalized * baseSpeed;
			} else if (magnitude > (baseSpeed * baseSpeed) * 2) {
				camScript.screen.shake (0.1f, 0.5f * ((baseSpeed * baseSpeed) * 2)/ magnitude);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Hitbox")) {			
			if ((invensibilityTimer == 0) && (other.transform.parent != transform)){
				hitboxScript hs = other.GetComponent<hitboxScript> ();
				if (hs != null) {
					AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);

					float knockback = hs.knockback;

					if (!state.IsName ("block")) {
						myAnim.SetBool ("hurt", true);
						hurtTimer = hurtCooldown * hs.knockback;
						invensibilityTimer = hurtCooldown / 2;
					} else {
						knockback /= 2;
					}

					camScript.screen.shake (0.1f, 0.5f * knockback);

					Transform father = other.transform.parent;
					float otherAngle = father.rotation.eulerAngles.z * Mathf.Deg2Rad;
					Vector2 dir = new Vector2 (Mathf.Cos (otherAngle), Mathf.Sin (otherAngle));

					rb.velocity = dir.normalized * knockback * 10f /weight;

					father.gameObject.GetComponent<Rigidbody2D> ().velocity = -dir.normalized * baseSpeed/2f * knockback;
					lastHitPlayer = father.gameObject.GetComponent<charScript>().playerId;

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
