using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class sortingLayerScript : MonoBehaviour {

	public string sortingLayer = "Default";
	// Use this for initialization
	void Update () {
		Renderer rend = GetComponent<Renderer>();
		rend.sortingLayerName = sortingLayer;
		rend.sortingOrder = 0;
	}
}
