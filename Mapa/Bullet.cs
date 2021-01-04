using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Vector3 direction;
	string creator;
	EnemyAttacked attacked;
	public GameObject bloodImpact, wallImpact;
	float timer = 10.0f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (direction*17*Time.deltaTime); //

		timer -= Time.deltaTime;
		if(timer<=0)
		{
			//Destroy (this.gameObject); //Destrói depois de 10 segundos se não atingir nada
		}

	}


	public void setVals(Vector3 dir, string name)
	{
		direction = dir;
		creator = name;
	}


	void OnCollisionEnter2D(Collision2D col) // Se atingir o gameobject, ele receberá o script do inimigo e depois será morto pela bala
	{
		if (col.gameObject.tag == "Enemy") {
			attacked = col.gameObject.GetComponent<EnemyAttacked> ();
			attacked.killBullet ();
			Instantiate (bloodImpact, transform.position, transform.rotation); //Cria sprite de sangue e destrói a bala
			Destroy (gameObject);


		} else if (col.gameObject.tag == "Player") {
			Instantiate (bloodImpact, transform.position, transform.rotation);
			PlayerHealth.dead = true;
			Destroy (gameObject);
		}

		else {
			Instantiate (wallImpact, transform.position, transform.rotation); //Se não atingiu o inimigo, ele apenas criará impacto na parede
			Destroy (gameObject);
		}
	}
}