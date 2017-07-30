using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class charScript : MonoBehaviour {

	//[Range(1, 4)]
	public int playerId = 1;
	public float hurtCooldown = 0.3f;
	public float specialCharge = 0.2f;
	public bool CPU = false;

	lightPunchScript ltPunchScr;
	heavyPunchScript hvPunchScr;
	dodgeScript dodgeScr;
	blockScript blockScr;
	specialScript specialScr;
	idleScript idleScr;

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
	public GameObject hitEffectPrefab;

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
	[HideInInspector]
	public float invensibilityTimer = 0f;
	float specialMeter = 0f;
	UIBarScript UIBar;

	float outlineSize = 0.5f;
	Color startOutlineColor;
	Color outlineColor;

	int lastHitPlayer = -1;
	[HideInInspector]
	public float life;

	bool dead = false;

	void Awake(){
		myAnim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		sprRenderer = GetComponent<SpriteRenderer> ();

		ltPunchScr = GetComponent<lightPunchScript>();
		hvPunchScr = GetComponent<heavyPunchScript>();
		specialScr = GetComponent<specialScript>();
		dodgeScr = GetComponent<dodgeScript>();
		blockScr = GetComponent<blockScript>();
		idleScr = GetComponent<idleScript>();

	}

	void Start () {
		startPos = transform.position;
		life = res;

		marker = Instantiate (markerPrefab,
			new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 0.7f),
			Quaternion.identity) as GameObject;
		marker.GetComponent<selectSprite> ().changeSprite (playerId - 1);

		if (UIBar == null) {
			UIBar = managerScript.manager.getUIBar (playerId);
		}

		switch (playerId) {
		case 1:
			startOutlineColor = new Color (1f, 0f, 0.2f, 0.2f);
			break;
		case 2:
			startOutlineColor = new Color (0f, 0.2f, 1f, 0.2f);
			break;
		case 3:
			startOutlineColor = new Color (0f, 0.7f, 0.1f, 0.2f);
			break;
		case 4:
			startOutlineColor = new Color (0.8f, 0.2f, 0f, 0.2f);
			break;
		default:
			startOutlineColor = new Color (0.5f, 0.2f, 0.8f, 0.2f);
			break;
		}

		outlineColor = startOutlineColor;
		UpdateShader ();
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
				Color colorWithoutAlpha = sprRenderer.color;
				if (hurtTimer == 0) {
					colorWithoutAlpha.a = 0.5f;
				}
				invensibilityTimer -= Time.deltaTime;
				if (invensibilityTimer <= 0) {
					invensibilityTimer = 0;
					colorWithoutAlpha.a = 1f;
				}
				sprRenderer.color = colorWithoutAlpha;
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
			if (!dead){
				Vector3 pos = new Vector3(Screen.width, Screen.height, 0);
				Vector3 screenPos = Camera.main.ScreenToWorldPoint(pos);

				if (Mathf.Abs(transform.position.x) > screenPos.x + 1f || Mathf.Abs(transform.position.y) > screenPos.y + 1f || life <= 0) {
					myAnim.Play("moving", 0, 0);
					myAnim.SetBool("hurt", false);
					dead = true;
					Invoke("die", 0.1f);
				}
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
		life = Mathf.Clamp (life + Time.deltaTime * 0.1f, 0, res);
		if (UIBar != null) {
			UIBar.setLifebar(life/res);
		}

	}

	void die(){

		Instantiate (crystalDustPrefab, transform.position, Quaternion.identity);
		camScript.screen.shake (0.1f, 0.5f);
		hurtTimer = 0f;

		transform.position = startPos;
		dead = false;

		Transform sparks = transform.Find("sparks(Clone)");
		if (sparks != null){
			Destroy(sparks.gameObject);
		}
		Transform beam = transform.Find("beamGenerator(Clone)");
		if (beam != null){
			Destroy(beam.gameObject);
		}

		managerScript.manager.addDeath(playerId);


		if (lastHitPlayer > 0) {
			managerScript.manager.givePoints (lastHitPlayer, 1);
			managerScript.manager.addKO(lastHitPlayer);
		} else if (lastHitPlayer < 0) {
			managerScript.manager.givePoints (playerId, -1);
			managerScript.manager.addSD(playerId);
		}

		lastHitPlayer = -1;
		rb.velocity = Vector2.zero;
		managerScript.manager.resetChar (playerId);
	}

	void idle(){

		if (!CPU){
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


			if (inputManager.inputman.Attack(playerId-1)) {
				if (timeToPunch > 0) {
					hvPunch ();
				} else {
					ltPunch ();
				}
			} else if (inputManager.inputman.Special(playerId-1)) {
				special ();
			} else if (inputManager.inputman.Block(playerId-1)) {
				block ();
			} else if (inputManager.inputman.Dodge(playerId-1)) {
				dodge ();
			}
		} else {
			idleScr.idle();
		}
	}

	public void ltPunch(){
		ltPunchScr.ltPunch();
	}

	public void hvPunch(){
		hvPunchScr.hvPunch();
	}

	public void special(){
		if (specialMeter == 1f) {

			specialScr.special();

			specialMeter = 0f;
			managerScript.manager.specialReset (playerId);

			outlineColor = startOutlineColor;
			outlineSize = 1f;
			UpdateShader ();
		}
	}

	public void block(){
		blockScr.block();
	}

	public void dodge(){
		dodgeScr.dodge();
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag ("Paddle")) {
			if (other.gameObject.GetComponent<paddleSettingsScript> ().playerId == playerId) {
				// Sólo si es mi paleta
				// El último que me pegó no es nadie
				lastHitPlayer = 0;
				//Recargo especial
				if (specialMeter < 1f){
					specialMeter += specialCharge;

					if (specialMeter >= 1f) {
						specialMeter = 1f;
						outlineColor.a = 1f;
						outlineSize = 2f;
						UpdateShader ();
					}
					UIBar.buttonToggle(specialMeter == 1f);
				}
			}

			// EXPERIMENTAL: normalizar velocidad
			if (!CPU && inputManager.inputman.CW(playerId-1) && inputManager.inputman.CCW(playerId-1)){
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

					float knockback = hs.knockback; // fuerza de knockback
					bool inverted = hs.invert; 		// Indica si va para el otro lado

					if (!state.IsName ("block")) {
						myAnim.SetBool ("hurt", true);
						hurtTimer = hurtCooldown * hs.knockback;
						invensibilityTimer = hurtCooldown / 2;
					} else {
						knockback /= 2;
					}

					camScript.screen.shake (0.1f, 0.5f * knockback);

					Transform father = other.transform.parent;

					float otherAngle = father.rotation.eulerAngles.z;
					if (inverted) {
						otherAngle -= 180;
						if (otherAngle < 0)
							otherAngle += 360;
					}
					otherAngle = otherAngle * Mathf.Deg2Rad;
					Vector2 dir = new Vector2 (Mathf.Cos (otherAngle), Mathf.Sin (otherAngle));

					rb.velocity = dir.normalized * knockback * 10f /weight;

					// Hacer que el que me pegó también tenga knockback
					father.gameObject.GetComponent<Rigidbody2D> ().velocity = -dir.normalized * baseSpeed/2f * knockback;
					charScript otherScript = father.gameObject.GetComponent<charScript> ();
					lastHitPlayer = otherScript.playerId; // el último que me pegó es el otro

					// Quita vida dependiendo de la fuerza del otro
					life = Mathf.Clamp (life - knockback * otherScript.str / 20f, 0, res);

					// Creo un effecto de golpe
					Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
				}
			}
		} else if (other.CompareTag ("Projectile")){

			GameObject father = other.gameObject.GetComponent<hitboxScript>().hitter;

			if ((father != gameObject) &&(invensibilityTimer == 0) && (other.transform.parent != transform)){
				hitboxScript hs = other.GetComponent<hitboxScript> ();
				if (hs != null) {
					AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);

					float knockback = hs.knockback; // fuerza de knockback
					bool inverted = hs.invert; 		// Indica si va para el otro lado

					if (!state.IsName ("block")) {
						myAnim.SetBool ("hurt", true);
						hurtTimer = hurtCooldown * hs.knockback;
						invensibilityTimer = hurtCooldown / 2;
					} else {
						knockback /= 2;
					}

					camScript.screen.shake (0.1f, 0.5f * knockback);

					float otherAngle = other.transform.rotation.eulerAngles.z;
					if (inverted) {
						otherAngle -= 180;
						if (otherAngle < 0)
							otherAngle += 360;
					}
					otherAngle = otherAngle * Mathf.Deg2Rad;
					Vector2 dir = new Vector2 (Mathf.Cos (otherAngle), Mathf.Sin (otherAngle));

					rb.velocity = dir.normalized * knockback * 10f /weight;


					charScript otherScript = father.GetComponent<charScript> ();
					lastHitPlayer = otherScript.playerId; // el último que me pegó es el otro

					// Quita vida dependiendo de la fuerza del otro
					life = Mathf.Clamp (life - knockback * otherScript.str / 20f, 0, res);

					// Creo un effecto de golpe
					Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
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

	void UpdateShader(){
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
		sprRenderer.GetPropertyBlock(mpb);
		//mpb.SetFloat("_Outline", outline ? 1f : 0f);
		mpb.SetColor("_OutlineColor", outlineColor);
		mpb.SetFloat("_OutlineSize", outlineSize);
		sprRenderer.SetPropertyBlock(mpb);
	}

	void OnDestroy(){
		if (marker != null) {
			Destroy (marker);
		}
	}

}
