using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
	GameObject player; // Cria o objeto player
	public bool followPlayer = true;
	Vector3 mousePos;
	PlayerMovement pm;
	Camera cam;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");// Procura a tag "player"
		pm = player.GetComponent<PlayerMovement>();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)) //Se a tecla SHIFT é pressionada, "stop movement" e a câmera começa a seguir o mouse
		{
			followPlayer = false;
			pm.setMoving(false);
		} else
		{
			followPlayer = true;
		}

		if (followPlayer == true) {
			camFollowPlayer ();
		} else
		{
			lookAhead();
		}
	}
	public void setFollowPlayer (bool val)
	{
		followPlayer = val;
	}

	void camFollowPlayer()

	{
		Vector3 newPos = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z); // pega a posição x e y dos jogadores e mantém a posição da câmera z, depois atribui à posição de transformação
		transform.position = newPos;

	}

	void lookAhead()
	{
		Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)); //pega a posição do mouse para mover a câmera
		camPos.z = -10; //Aumenta a visualização da tela
		Vector3 dir = camPos - transform.position; //Direção entre o eixo do jogador e do mouse
		if (player.GetComponent<SpriteRenderer>().isVisible == true) //Se o sprite do jogador estiver visível, mova até ficar invisível para a câmera
		{
			transform.Translate(dir * 2 * Time.deltaTime); 
		}

	}


}