using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBackgroundScript : MonoBehaviour {

	public Material mat;
	public Vector2 scrollSpeed;
	Renderer rend;

	void Start(){
		rend = GetComponent<Renderer>();
	}

	void Update() {
		Vector2 offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offset.x, offset.y));
	}
}
