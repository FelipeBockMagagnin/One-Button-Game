﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMov : MonoBehaviour {

	Animator playerAnim;
	AudioSource monsterDieSound;
	AudioSource swingSound;
	DifficultyManager difficultyScript;
	StyleManager styleScript;
	ChangeBackgroundColor changeScript;

	void Start(){
		changeScript = GameObject.Find("MainCamera").GetComponent<ChangeBackgroundColor>();
		styleScript = GameObject.Find("StyleManager").GetComponent<StyleManager>();
		difficultyScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
		monsterDieSound = GameObject.Find("MonsterDieSound").GetComponent<AudioSource>();
		swingSound = GameObject.Find("MoveSound").GetComponent<AudioSource>();
        playerAnim = this.GetComponent<Animator>();

        changeScript.FirstColor();
    }

	void Update () {
		
		if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began && styleScript.gameStarted == true){
				transform.Rotate (0,0,-90);
				playerAnim.SetTrigger("move");
				swingSound.Play();
			}
		}

        //just testing input on pc
        if(Input.GetMouseButtonDown(0) && styleScript.gameStarted == true)
        {
            transform.Rotate(0, 0, -90);
            playerAnim.SetTrigger("move");
            swingSound.Play();
        }
	}


	void OnTriggerEnter2D(Collider2D collider){		
		styleScript.InstantiateParticle(collider.transform);
		Destroy(collider.gameObject);
		difficultyScript.camAnim.SetTrigger("shake");
		HighScoreManager.points++;
		monsterDieSound.Play();
		changeScript.changeBackGround();
	}

	public void reloadLevel(){
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}
}