﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour {

    public Sprite[] walking, attacking, legsSpr;
	int counter = 0, legCount = 0;
	PlayerMovement pm;
	float timer = 0.05f, legTimer = 0.05f;
	public SpriteRenderer torsoSprite, legsSprite;
	SpriteContainer sc;
	bool attackingB = false;
    // Use this for initialization

    void Start () {
		pm = GetComponent<PlayerMovement> ();
		sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SpriteContainer> ();
		walking = sc.getPlayerUnarmedWalk ();
		legsSpr = sc.getPlayerLegs ();
		attacking = sc.getPlayerPunch ();
		
	}
	
	// Update is called once per frame
	void Update () {
		animateLegs ();
		if (attackingB == false) {
			animateTorso ();
		} else {
			animateAttack ();
		}
	}

	void animateTorso()
	{
		if (pm.moving == true) { //Verifica se o player anda
			torsoSprite.sprite = walking [counter];
			timer -= Time.deltaTime;
			if (timer <= 0) {
				if (counter < walking.Length - 1) {
					counter++;
				} else {
					counter = 0;
				}
				timer = 0.1f;
			}
		}
	}

	void animateAttack(){
		torsoSprite.sprite = attacking [counter];
			timer -= Time.deltaTime;
		
			if (timer <= 0) {
			if (counter < attacking.Length - 1) {
				counter++;
				} else {
				if (attackingB == true) {
					attackingB = false;
				}
					counter = 0;
				}
				timer = 0.05f;
			}
		}


	void animateLegs()
	{
		if (pm.moving == true) {
			legsSprite.sprite = legsSpr [legCount];
			legTimer -= Time.deltaTime;
			if (legTimer <= 0) {
				if (legCount < legsSpr.Length - 1) {
					legCount++;
				} else {
					legCount = 0;
				}
				legTimer = 0.1f;
			}
		}
	}

	public void attack()
	{
		attackingB = true;
	}
	public void resetCounter()
	{
		counter = 0;
		attackCheck ();
	}

	void attackCheck(){
		if (attackingB == false) {
			torsoSprite.sprite = walking[0];
		}
	}
	public bool getAttack(){
		return attackingB;
	}


	public void resetSprites()
	{
		counter = 0;
		walking = sc.getPlayerUnarmedWalk ();
		attacking = sc.getPlayerPunch ();
		torsoSprite.sprite = walking[0];
	}

	public void setNewTorso(Sprite[] walk, Sprite[] attack)
	{
		counter = 0;
		attacking = attack;
		walking = walk;
		torsoSprite.sprite = walking [0];
	}



}
