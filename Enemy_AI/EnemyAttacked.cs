using UnityEngine;
using System.Collections;

public class EnemyAttacked : MonoBehaviour {
	public Sprite knockedDown,stabbed,bulletWound,backUp;
	public GameObject bloodPool,bloodSpurt; 
	SpriteRenderer sRender;
	bool EnemyKnockedDown=false;
	float knockDownTimer = 3.0f;
	GameObject player;
	ScoreController sControl;


	void Start () {
		sRender = GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		sControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreController> ();
	}


	void Update () {
		if(EnemyKnockedDown==true){
			knockDown ();
		}
	}

	public void knockDownEnemy()
	{
		EnemyKnockedDown = true;
		Debug.Log ("OOF");
	}

	void knockDown()
	{



		knockDownTimer -= Time.deltaTime;
		sRender.sprite = knockedDown;
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<EnemyState> ().enabled = false;

		if (knockDownTimer <= 0) {
			sControl.AddScore (500,transform.position);
			EnemyKnockedDown = false;
			sRender.sprite = backUp;
			GetComponent<EnemyState> ().enabled = true;
			GetComponent<CircleCollider2D> ().enabled = true;

			knockDownTimer = 3.0f;
		}

	}

	public void killBullet()
	{
		sControl.AddScore (500,transform.position);
		sRender.sprite = bulletWound;
		Instantiate (bloodPool, transform.position, transform.rotation);
		GetComponent<EnemyState> ().enabled = false;
		GetComponent<CircleCollider2D>().enabled= false;
		gameObject.tag = "Dead";

	}

	public void killMelee()
	{
		sControl.AddScore (1000,transform.position);
		sRender.sprite = stabbed;
		Instantiate (bloodPool,transform.position,transform.rotation);
		Instantiate (bloodSpurt,transform.position,player.transform.rotation);
		sRender.sortingOrder = 2;
		GetComponent<EnemyState> ().enabled = false;
		GetComponent<CircleCollider2D>().enabled= false;
		gameObject.tag = "Dead";
	}


}
