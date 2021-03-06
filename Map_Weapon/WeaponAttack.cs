﻿using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour {
	public GameObject oneHandSpawn, twoHandSpawn;
	Bullet bl;
	GameObject curWeapon;
	public bool gun = false;
	float timer = 0.1f,timerReset=0.1f;
	PlayerAnimate pa;
	SpriteContainer sc;

	float weaponChange = 0.5f;
	bool changingWeapon = false;
	bool oneHanded = false;

	void Start () {
		pa = GetComponent<PlayerAnimate> ();
		sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SpriteContainer> ();
		oneHandSpawn = GameObject.Find ("oneHandSpawn");
		bl = GameObject.Find ("Bullet").GetComponent<Bullet> ();


	}


	void Update () {


		if (timer > 0) {
			timer -= Time.deltaTime;
		}

		if(Input.GetMouseButton(0) && timer <=0)
		{
			attack ();
		}
		if(Input.GetMouseButtonDown(0))
		{
			pa.resetCounter ();
		}
		if (Input.GetMouseButtonUp (0)) {
			pa.resetCounter ();
		}

		if (Input.GetMouseButtonDown (1) && changingWeapon == false) {
			dropWeapon ();
		}

		if(changingWeapon==true)
		{
			weaponChange -= Time.deltaTime;
			if(weaponChange<=0)
			{
				changingWeapon = false;
			}
		}
	}

	public void setWeapon(GameObject cur, string name, float fireRate,bool gun, bool oneHanded)
	{
		GetComponent<AudioController> ().pickupWeapon();
		changingWeapon = true;
		curWeapon = cur;
		pa.setNewTorso (sc.getWeaponWalk(name),sc.getWeapon(name));
		this.gun = gun;
		timerReset = fireRate;
		timer = timerReset;
		this.oneHanded = oneHanded;
	}


	public void attack()
	{


		pa.attack ();
		if (gun == true) {
			//Bullet bl = bullet.GetComponent<Bullet> ();//creation of bullet with own direction set 
			Vector3 dir;
			dir.x = Vector2.right.x; //rotation of bullet 
			dir.y = Vector2.right.y;
			dir.z = 0;
			bl.setVals (dir, "Player"); //knows who created the bullet 
			if (oneHanded == true) {
				Instantiate (bl, oneHandSpawn.transform.position, transform.rotation);
			} else {
				Instantiate (bl, twoHandSpawn.transform.position, transform.rotation);
			}
			GetComponent<AudioController> ().fireSmg ();

			timer = timerReset;
		} else {
			//melee attack
			int layerMask = 1<<15;
			layerMask = ~layerMask;
			pa.attack ();
			RaycastHit2D ray = Physics2D.Raycast (new Vector2(transform.position.x,transform.position.y),new Vector2(transform.right.x,transform.right.y),1.5f, layerMask); //create a line from the player wich can hit an enemy 
			Debug.DrawRay (new Vector2(transform.position.x,transform.position.y),new Vector2(transform.right.x,transform.right.y),Color.green);
			if (curWeapon == null && ray.collider.gameObject.tag == "Enemy") { // if player doesn't hold any weapon and the player sight line (ray) hits enemy, 
				EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked> ();
				ea.knockDownEnemy ();
				GetComponent<AudioController> ().meleeAttack();
			} else if (ray.collider != null) {
				if (ray.collider.gameObject.tag == "Enemy") { //if player have meelee weapon -> instant kill 
					EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked> ();
					ea.killMelee ();
					GetComponent<AudioController> ().meleeAttack();
				}
			}
		}

	}

	public GameObject getCur()
	{
		return curWeapon;
	}

	public void dropWeapon()
	{

		if (curWeapon == null) {

		} else {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0)); //get the pos of the mouse and move the weapon in that way
			curWeapon.AddComponent<ThrowWeapon> ();
			Vector3 dir;
			dir.x = mousePos.x - transform.position.x;
			dir.y = mousePos.y - transform.position.y;
			dir.z = 0;
			curWeapon.GetComponent<Rigidbody2D> ().isKinematic = false; //can be affected by physics
			curWeapon.GetComponent<ThrowWeapon> ().setDirection (dir);
			curWeapon.transform.position = oneHandSpawn.transform.position;
			curWeapon.transform.eulerAngles = transform.eulerAngles;
			curWeapon.SetActive (true);
			setWeapon (null, "", 0.5f, false,false);
			pa.resetSprites ();
		}

	}






}