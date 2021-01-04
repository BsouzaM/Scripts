using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

	// Ao herdar essa interface, todas as classes dos estados inimigos terão esses métodos,
	// então eles vão trabalhar com enemystate, sem o enemystate saber nada sobre as state classes.
	// esses métodos são para alterar os estados dentro do EnemyState através do currentState (digite IEnemyState).

	void UpdateState();

	void OnTriggerEnter(Collider other);

	void ToPatrolState();

	void ToAlertState();

	void ToChaseState();
}
