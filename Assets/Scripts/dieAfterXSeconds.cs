using UnityEngine;
using System.Collections;

public class dieAfterXSeconds : MonoBehaviour {

	public float time = 1f;

	void Start () {
		Destroy (gameObject, time);
	}
}
