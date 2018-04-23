using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bouncySpriteScript : MonoBehaviour {

	[Range(-50, 50)]
	public float factor = 20f;
	[Range(-20, 20)]
	public float frequency = 10f;
	[Range(-50, 50)]
	public float rotFactor = 2f;
	[Range(-20, 20)]
	public float rotFrequency = 3f;

	[Space(10)]
	[Range(-1, 1)]
	public float xFactor = 0f;
	[Range(-20, 20)]
	public float xFrequency = 0f;
	[Range(-1, 1)]
	public float yFactor = 0f;
	[Range(-20, 20)]
	public float yFrequency = 0f;

	private RectTransform img;
	private Vector2 imgSize;
	private Vector2 imgPos;
	private float t1, t2, t3, t4;

	private Animator anim;

	void Start(){
		img = GetComponent<RectTransform>();
		imgSize = img.sizeDelta;
		imgPos = img.transform.position;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (anim){
			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || !anim.IsInTransition(0)){
				imgPos = img.transform.position;
			}
		}

		t1 = (1 + Mathf.Sin(Time.time * frequency))/2f;
		float meh = Mathf.SmoothStep(-factor, 0, t1);
		img.sizeDelta = imgSize - new Vector2(meh, meh);

		t2 = Mathf.Sin(Time.time * rotFrequency) * rotFactor;
		img.transform.rotation = Quaternion.Euler(0, 0, t2);

		t3 = Mathf.Sin(Time.time * xFrequency);
		t4 = Mathf.Sin(Time.time * yFrequency);
		img.transform.position = imgPos + new Vector2(xFactor * t3, yFactor * t4);
	}
}
