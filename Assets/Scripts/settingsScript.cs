using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class settingsScript : MonoBehaviour {

	public static settingsScript settings;

	public GameObject[] characters;
	public bool[] autoPaddles;
	public bool[] bigPaddles;

	//match settings
	[Space(10)]
	public bool stockBattle = false;
	public int maxLives;		// maxLives that each player has
	public int matchTime;		// match time in minutes. 0 for endless.
	[Space(10)]
	public float musicVolume = 1.0f;
	public float soundVolume = 1.0f;


	private int winner = -1;

	int[] scores;
	int[] KOs;
	int[] SDs;
	int[] deaths;

	void Awake () {
		if (settings == null){
			DontDestroyOnLoad(gameObject);
			settings = this;

			resetScores();
		} else if (settings != this){
			Destroy(gameObject);
		}
		autoPaddles = new bool[4];
		for (int i = 0; i < 4; ++i){
			autoPaddles[i] = false;
		}

		bigPaddles = new bool[4];
		for (int i = 0; i < 4; ++i){
			bigPaddles[i] = true;
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

	public void setWinner(int w){
		winner = w;
	}
	public int getWinner(){
		return winner;
	}

	public int getPlayerNumber(){
		int n = 0;
		foreach (GameObject ch in characters){
			if (ch != null){
				n++;
			}
		}
		return n;
	}

	public void setScore(int id, int score){
		scores[id] = score;
	}
	public void setKOs(int id, int ko){
		KOs[id] = ko;
	}
	public void setSDs(int id, int sd){
		SDs[id] = sd;
	}
	public void setDeaths(int id, int deathnum){
		deaths[id] = deathnum;
	}

	public int getScore(int i){
		return scores[i];
	}
	public int getKO(int i){
		return KOs[i];
	}
	public int getSD(int i){
		return SDs[i];
	}
	public int getDeaths(int i){
		return deaths[i];
	}

	public void resetScores(){
		scores = new int[4];
		KOs = new int[4];
		SDs = new int[4];
		deaths = new int[4];
	}

}

[Serializable]
class matchData{
	public int maxLives;
	public int matchTime;
}
