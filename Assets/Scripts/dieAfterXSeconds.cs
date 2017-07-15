using UnityEngine;
using System.Collections;

public class dieAfterXSeconds : MonoBehaviour {

	public float time = 1f;
	public GameObject effect;

	void Start () {
		Invoke("die", time);
	}

	void die(){
		if (effect != null){
			Instantiate(effect, transform.position, Quaternion.identity);
		}
		Destroy (gameObject);
	}
}
