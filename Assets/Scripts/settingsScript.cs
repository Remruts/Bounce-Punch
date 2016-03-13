﻿using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class settingsScript : MonoBehaviour {

	public static settingsScript settings;

	public GameObject[] characters;

	//match settings
	public int maxLives;		// maxLives that each player has
	public int matchTime;		// match time in minutes. 0 for endless.

	void Awake () {
		if (settings == null){
			DontDestroyOnLoad(gameObject);
			settings = this;
		} else if (settings != this){
			Destroy(gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/matchSettings.dat");

		matchData data = new matchData();
		data.maxLives = maxLives;
		data.matchTime = matchTime;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/matchSettings.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/matchSettings.dat", FileMode.Open);
			matchData data = (matchData) bf.Deserialize(file);
			file.Close();

			maxLives = data.maxLives;
			matchTime = data.matchTime;
		}
	}

}

[Serializable]
class matchData{
	public int maxLives;
	public int matchTime;
}