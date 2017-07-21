using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomButtonScript : MonoBehaviour {

	public GameObject[] character;
	public GameObject[] AICharacter;
	public Sprite portrait;

	public int getCharSeed(){
		return Random.Range(0, character.Length);
	}
}
