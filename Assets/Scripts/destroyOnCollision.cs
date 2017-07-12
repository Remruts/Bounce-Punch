using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnCollision : MonoBehaviour {

	GameObject hitter;
	public GameObject explosion;

	void Start(){
		hitter = GetComponent<hitboxScript>().hitter;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")){
			if (hitter != other.gameObject){
				Destroy(gameObject);
				if (explosion != null){
					Instantiate(explosion, transform.position, Quaternion.identity);
				}
			}
		}
	}
}
