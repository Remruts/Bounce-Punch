using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicManagerScript : MonoBehaviour {

	public static musicManagerScript musicman;
	AudioSource music;

	public AudioClip mainMenuMusic;
	public AudioClip characterSelectMusic;
	public AudioClip resultsMusic;
	public AudioClip gameplayMusic;

	string playing;

	void Awake () {
		if (musicman == null){
			musicman = this;
			DontDestroyOnLoad(gameObject);
		} else {
			if (musicman != this){
				Destroy(gameObject);
				return;
			}
		}
		music = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "mainMenuScene" && playing != "mainMenuMusic"){
			playing = "mainMenuMusic";
			music.clip = mainMenuMusic;
			music.Play();
		} else if ((scene.name == "optionsScene" || scene.name == "controllerScene" || scene.name == "playerSelectScene" || scene.name == "selectStageScene") && playing != "characterSelectMusic"){
			playing = "characterSelectMusic";
			music.clip = characterSelectMusic;
			music.Play();
		} else if ((scene.name == "resultsScene" || scene.name == "creditsScene") && playing != "resultsMusic"){
			playing = "resultsMusic";
			music.clip = resultsMusic;
			music.Play();
		} else if ((scene.name == "gameScene" || scene.name == "gameScene2" || scene.name == "gameScene3") && playing != "gameplayMusic"){
			playing = "gameplayMusic";
			music.clip = gameplayMusic;
			music.Play();
		}
		music.loop = true;
		music.volume = settingsScript.settings.musicVolume;
	}
}
