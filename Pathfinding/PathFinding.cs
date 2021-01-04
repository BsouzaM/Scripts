using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PathFinding : MonoBehaviour {
	public GameObject Enemy;
	public GameObject Player;
	public Sprite SpriteWall; // Arrasta o sprite de walls lá no Unity (a box que aparece pra você mover a sprite)
	public Sprite SpriteFloor; 
	public GameObject TileLayer;
	static int rows = 10;
	static int cols = 10;
	int[,] map = new int [rows, cols];

	void TileState (GameObject tile, int[,] array) {
		int x = (int)tile.transform.position.x; // a posição do objeto nos dá os índices do node correspondente no array do mapa
		int y = (int)Mathf.Abs(tile.transform.position.y);

		if (tile.GetComponent<SpriteRenderer> ().sprite == SpriteWall) { // agora é perguntado se o sprite é usado nesse objeto
			array [y, x] = 1; // solid tile/node

		} else {
			array [y, x] = 0; // open tile/node
		}
	}
		

	void Start ()
	{
		foreach (Transform child in TileLayer.transform) { // itera através de cada child dentro de nossa tile map layer (um objeto pai, funciona como um child)
			TileState (child.gameObject, map); // Agora cria o array de nodes
		}

		/* for (int i = 0; i < rows; i++) {
			for (int k = 0; k < cols; k++) {
				Debug.Log (map [i, k]);
			} 

		}*/

	}


	void Update() {
		var graph = new Graph (map);
		var search = new Search (graph);
		int EX = (int)Enemy.transform.position.x; // A posição X do inimigo mostra uma unidade menor do que a posição real da tile, mas foi levada em consideração na search.Start()
		int EY = (int)Mathf.Abs (Enemy.transform.position.y); 
		int PX = (int)Player.transform.position.x; // A posição X do jogador mostra uma unidade menor do que a posição real da tile, mas foi levada em consideração na search.Start()
		int PY = (int)Mathf.Abs (Player.transform.position.y); 

		search.Start (graph.nodes [(EX - 1) * cols + EY], graph.nodes [(PX - 1) * cols + PY]);


		while (!search.finished) {
			search.Step ();
		}


		for (int i = 0; i < search.path.Count; i++) {
			// Debug.Log ();
		}
		Debug.Log ("Search done. Path length " + search.path.Count + ", iterations " + search.iterations);	
		
		}

}

