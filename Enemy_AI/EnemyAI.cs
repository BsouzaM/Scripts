using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject player;
	public bool patrol = false;
	public bool clockwise = false;
	public bool pursuit = false;
	public bool goingToLastLoc = false;
	Vector3 target;
	Rigidbody2D rigid;
	public Vector3 playerLastPos;
	RaycastHit2D hit;

	float speed = 1.5f; 
	int layerMask = 1 << 7;


	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		patrol = true;

		rigid = GetComponent<Rigidbody2D> (); // RigidBody do inimigo
		layerMask = ~layerMask;

	}



	RaycastHit2D PlayerDetectionRay (){
		float dist = Vector3.Distance (player.transform.position, transform.position); // Direção do inimigo para o player
		Vector3 dir = player.transform.position - transform.position; // Direção em volta do player
		hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (dir.x, dir.y), dist, layerMask); // Cria um raycast do inimigo para o player
		Debug.DrawRay(transform.position, dir, Color.red); // Linha vermelha
		return hit;
	}

	RaycastHit2D EnemySightLine() {
		Vector3 sightDir = transform.TransformDirection(Vector3.right);  // Direção ao qual o inimigo olha (para frente)
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(sightDir.x, sightDir.y), 1.0f,layerMask);
		Debug.DrawRay (new Vector2(transform.position.x, transform.position.y), new Vector2(sightDir.x, sightDir.y), Color.cyan);
		return hit2;
	}
		

	void Move()
	{
		if(goingToLastLoc==true)
		{
			Debug.Log ("Vá para a última posição");
			speed = 3.0f;
			rigid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
			if (Vector3.Distance (transform.position, playerLastPos) < 1.5f) {
				patrol=true;
				goingToLastLoc = false;
			}



		}

	}

	public void PlayerDetect()
	{
		Vector3 pos = transform.InverseTransformPoint(player.transform.position);


		if(hit.collider!=null)
		{
			if (hit.collider.gameObject.tag == "Player" && pos.x > 1.2f && Vector3.Distance(transform.position,player.transform.position)<9) {
				patrol=false;
				pursuit = true;

			} else {
				if(pursuit==true)
				{
					goingToLastLoc = true;
					pursuit = false;

				}
			}
		}
	}

	public float getSpeed()
	{
		return speed;
	}



}