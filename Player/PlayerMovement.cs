using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool moving = false; // Controla a animação
	float speed = 5.0f; // Altere o valor do float speed para decidir o quão rápido o player irá se mover

	// Update is called once per frame
	void Update() {
		if (moving == true)
		{
			move();
		}
		MCheck();

        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = new Vector3(20, 9, 7);
            print("O jogador EVE foi teletransportado para o fim do jogo.");
        }
    }

	public void setMoving (bool val)
	{
		moving = val;
	}

	void move() {
		if (Input.GetKey(KeyCode.UpArrow)) { // Detecta se é key down ou up 
			transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World); 
            // Time.deltaTime para aumentar a performance em computadores antigos. 
            // Space.world conta esse movimento em relação ao world space, porque pode se mover em torno da rotação do cursor

			moving = true; // Para detectar o movimento
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
			moving = true;
		}

    }


	void MCheck()
	{
		if (Input.GetKey (KeyCode.RightArrow) != true && Input.GetKey(KeyCode.UpArrow) != true && Input.GetKey(KeyCode.DownArrow) != true && Input.GetKey(KeyCode.LeftArrow) != true)
		{ moving = false; }
		else
		{
			moving = true;
		}
	}
}