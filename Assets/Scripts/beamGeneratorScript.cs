using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamGeneratorScript : MonoBehaviour {

	public GameObject beamPart;
	public GameObject hitter;
	public int size = 10;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < size; ++i){
			GameObject beam = Instantiate(beamPart, transform.position + transform.right * i * 0.2f + transform.right * 0.1f, transform.rotation) as GameObject;
			hitboxScript hb = beam.GetComponent<hitboxScript>();
			hb.hitter = hitter;
			beam.transform.parent = transform;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
