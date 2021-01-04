using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlertState : IEnemyState {
	public readonly EnemyState enemy;
	private float searchTimer;

	public AlertState(EnemyState enemyState){
		enemy = enemyState;
	}

	public void OnTriggerEnter(Collider other){

	}

	public void ToAlertState() {

	}

	public void ToChaseState() {
		enemy.currentState = enemy.chaseState;
	}

	public void ToPatrolState(){
		enemy.currentState = enemy.patrolState;
	}

	private RaycastHit2D PlayerDetectionRay (){ // A linha do inimigo para o player
		float dist = Vector3.Distance (enemy.Target.transform.position, enemy.transform.position); // Distância do player para o inimigo
		Vector3 dir = enemy.Target.transform.position - enemy.transform.position; // Direção para o jogador
		enemy.Hit = Physics2D.Raycast (new Vector2 (enemy.transform.position.x, enemy.transform.position.y),
			new Vector2 (dir.x, dir.y), dist, enemy.LayerMask); // Cria um raycast do inimigo para o player
		Debug.DrawRay(enemy.transform.position, dir, Color.red); // Você verá o raio vermelho na visão da câmera ao executar o jogo
		return enemy.Hit;
	}

	private RaycastHit2D EnemySightLine() { // visão inimiga, bem curta
		Vector3 sightDir = enemy.transform.TransformDirection(Vector3.right); // Direção para qual o inimigo está olhando (pra frente)
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y),
			new Vector2(sightDir.x, sightDir.y), 1.0f, enemy.LayerMask); // Cria um raycast para representar a visão do inimigo
		Debug.DrawRay (new Vector2(enemy.transform.position.x, enemy.transform.position.y), new Vector2(sightDir.x, sightDir.y), Color.cyan); // raio azul na câmera
		return hit2;
	}

	private void Search(){
		enemy.transform.Rotate (Vector3.up * searchTimer *Time.deltaTime); // Deixa o inimigo lento
		searchTimer += Time.deltaTime; // Conta o tempo

		if (searchTimer >= enemy.SearchDuration) { // se o inimigo não ver nada, ele volta a patrulhar
			ToPatrolState ();
		}


	}

	public void UpdateState(){
		EnemySightLine ();
		PlayerDetectionRay ();
		Search ();
	}

}
