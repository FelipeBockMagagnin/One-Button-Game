﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleManager : MonoBehaviour {

	public  GameObject[] MainCharStyles; 			//0 - PixelArt; 1 - Pen; 2 - WaterColor
	public  Transform MainCharSpawn;	 			//Store the location of the spawn point

	public ParticleSystem[] ParticleStyle;			//0 - PixelArt; 1 - Pen; 2 - WaterColor
	public ParticleSystem[] StartParticleSystem;	//0 - PixelArt; 1 - Pen; 2 - WaterColor

	public  GameObject ActualMainChar;		//store the last mainchar instantiated	
	public GameObject ActualStartParticle;	//store the last particle instantiated
	public  int index = 0;					//number of the last mainchar

	public bool gameStarted;				//define if the game started
	

	public Button startButton;	
	public SpawnManager spawnScript;			

	void Start(){
		spawnScript = GameObject.Find("Spawn").GetComponent<SpawnManager>();
		gameStarted = false;
		index = HighScoreManager.index;
		MainCharSpawn = GameObject.Find("MainCharSpawn").transform;
		if(ActualMainChar != null){
			Destroy(ActualMainChar);
		}
		ActualMainChar = Instantiate(MainCharStyles[index], MainCharSpawn.position, Quaternion.identity);
		ActualStartParticle = Instantiate(StartParticleSystem[HighScoreManager.index].gameObject, MainCharSpawn.position, Quaternion.identity);
	}

	public void SetGameStartedTrue(){
		if(gameStarted == false){
			spawnScript.StartGame(HighScoreManager.index);
		}
		gameStarted = true;
		
	}

	public void DestroyParticle(){
		if(ActualStartParticle != null){
			Destroy(ActualStartParticle);
		}
	}

	public void ChangeStartParticle(){
		if(ActualStartParticle != null){
			Destroy(ActualStartParticle);
		}
		ActualStartParticle = Instantiate(StartParticleSystem[HighScoreManager.index].gameObject, MainCharSpawn.position, Quaternion.identity);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space) && gameStarted == false){
			startButton.onClick.Invoke();
			gameStarted = true;
			if(ActualStartParticle != null){
				Destroy(ActualStartParticle);
			}
		}
	}

	void setGameStartedTrue(){
		gameStarted = true;
	}

	public void InstantiateParticle(Transform enemyPosition){
		Instantiate(ParticleStyle[HighScoreManager.index], enemyPosition.position, Quaternion.identity);
	}

	public void WatercolorChars(){
		if(ActualMainChar != null){
			Destroy(ActualMainChar);
		}
		ActualMainChar = Instantiate(MainCharStyles[2], MainCharSpawn.position, Quaternion.identity);
		HighScoreManager.index = 2;
	}

	public void PenChars(){
		if(ActualMainChar != null){
			Destroy(ActualMainChar);
		}
		ActualMainChar = Instantiate(MainCharStyles[1], MainCharSpawn.position, Quaternion.identity);
		HighScoreManager.index = 1;
	}

	public void PixelChars(){
		if(ActualMainChar != null){
			Destroy(ActualMainChar);
		}
		ActualMainChar = Instantiate(MainCharStyles[0], MainCharSpawn.position, Quaternion.identity);
		HighScoreManager.index = 0;
	}
}
