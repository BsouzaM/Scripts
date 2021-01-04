using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateEffect : MonoBehaviour {
	PlayerMovement pm;

	float mod = 0.1f;
	float zVal = 0.0f;
	// Use this for initialization
	void Start () {
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update () {
		if (pm.moving==true) //Se o player estiver movendo, irá criar um vector de rotação
		{
			Vector3 rot = new Vector3(0, 0, zVal); //Gira somente na direção x e y
			transform.eulerAngles = rot;


			zVal += mod;


			if (transform.eulerAngles.z >= 5.0f && transform.eulerAngles.z < 10.0f)  //Rotação ao redor
			{
				mod = -0.1f;
			}
			else if (transform.eulerAngles.z < 355.0f && transform.eulerAngles.z > 350.0f)
			{ mod = 0.1f; }
		}

	}
}