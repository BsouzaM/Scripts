using UnityEngine;
using System.Collections;

public class ThrowWeapon : MonoBehaviour {
	EnemyAttacked attacked;
	float timer = 2.0f;
	Vector3 direction;
	Rigidbody2D rid;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rid = GetComponent<Rigidbody2D> ();
		rid.AddForce (direction*40);
	}

	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Slerp(transform.rotation,new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z-1,transform.rotation.w), Time.deltaTime * 10);
		timer -= Time.deltaTime;
		if(timer<=0)
		{
			rid.isKinematic = true;
			Destroy (this);
		}
	}


	public void setDirection(Vector3 dir)
	{
		direction = dir;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			attacked = col.gameObject.GetComponent<EnemyAttacked> ();
			attacked.knockDownEnemy();
			rid.isKinematic = true;
			Destroy (this);
		}
		else if(col.gameObject.tag=="Player")
		{

		}

		else {
			rid.isKinematic = true;
			Destroy (this);
		}
	}
}
