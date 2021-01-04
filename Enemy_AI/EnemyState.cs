using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	[HideInInspector]
	public IEnemyState currentState;

	[HideInInspector]
	public PatrolState patrolState;

	[HideInInspector]
	public AlertState alertState;

	[HideInInspector]
	public ChaseState chaseState;

	// Esses são valores utilizados em outras classes.
	// Uma vez que o EnemyState é a única classe que herda do monobehaviour, é a única classe que pode acessar objetos no Unity.
	// É por isso que eu coloquei esses valores antes, e eles são chamados em outras classes através do inimigo (EnemyState).
	// Dessa forma você não precisa abrir os scripts se quiser mudar ou tentar valores diferentes.

	public float Speed = 1.5f;
	public float ChaseSpeed = 5.5f;
	public GameObject Target;
	public bool Clockwise;
	public RaycastHit2D Hit;

	[HideInInspector]
	public int LayerMask = 1 << 8;

	public bool Stationary;
	public float SearchDuration;
	public float RotationSpeed = 5.5f;
	public float MinDist;
	public float SlowDownDist;

	void Awake (){
		patrolState = new PatrolState(this);
		alertState = new AlertState(this);
		chaseState = new ChaseState(this);

	}
		
	void Start(){
		currentState = patrolState; // O guardinha começa a patrulhar
		Target = GameObject.Find("Player");
		LayerMask = ~LayerMask;
	}

	void FixedUpdate() {
		currentState.UpdateState ();
	}

	private void onTriggerEnter(Collider other){
		currentState.OnTriggerEnter (other);
	}

}
