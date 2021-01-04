using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolState : IEnemyState {
	private readonly EnemyState enemy;

	public PatrolState(EnemyState enemyState) {
		enemy = enemyState;
	}

	public void UpdateState (){
		Patrol();
		EnemySightLine ();
		PlayerDetectionRay ();
	}

	void Patrol () {
		enemy.transform.Translate(Vector3.right * enemy.Speed * Time.deltaTime);

		if (EnemySightLine().collider.gameObject.tag == "Wall"){
			Debug.Log ("Eu vi uma parede! Retornando.");
			if (enemy.Clockwise == false){
				enemy.transform.Rotate(0, 0, 90);
			} else {
				enemy.transform.Rotate(0, 0, -90);
			}
		}
			

		if(PlayerDetectionRay().collider.gameObject.tag == "Player") {
			ToChaseState();
			Debug.Log("Achei o player");
		}			
	}

	private RaycastHit2D PlayerDetectionRay (){ // A linha do inimigo para o player
		float dist = Vector3.Distance (enemy.Target.transform.position, enemy.transform.position); // Distância do player para o inimigo
		Vector3 dir = enemy.Target.transform.position - enemy.transform.position; // Direção em volta do player
		enemy.Hit = Physics2D.Raycast (new Vector2 (enemy.transform.position.x, enemy.transform.position.y),
			new Vector2 (dir.x, dir.y), dist, enemy.LayerMask); // Cria um raycast do inimigo para o player
		Debug.DrawRay(enemy.transform.position, dir, Color.red); // Você vai ver uma linha vermelha
		return enemy.Hit;
	}

	private RaycastHit2D EnemySightLine() { // Visão do inimigo, bem pequena
		Vector3 sightDir = enemy.transform.TransformDirection(Vector3.right); // Direção ao qual o inimigo olha (para frente)
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y),
			new Vector2(sightDir.x, sightDir.y), 1.0f, enemy.LayerMask);  // Cria um raycast para representar a visão do inimigo
		Debug.DrawRay (new Vector2(enemy.transform.position.x, enemy.transform.position.y), new Vector2(sightDir.x, sightDir.y), Color.cyan); // Raio azul
		return hit2;
	}

	public void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			ToAlertState ();
		}
	}


	public void ToAlertState(){
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState(){
		enemy.currentState = enemy.chaseState;
	}


	public void ToPatrolState(){
		// Não será usado, mas é melhor deixar em branco por causa do IEnemyState
	}
		
}
