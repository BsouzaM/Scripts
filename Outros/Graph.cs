using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

	public int rows = 0;
	public int cols = 0;

	public Node[] nodes; //Cria um array dos nodes que a gente quer acompanhar

	public Graph(int[,] grid){
		rows = grid.GetLength (0);
		cols = grid.GetLength (1);

		nodes = new Node[grid.Length];

		// Loop através de todos os lugares do array e cria um node vazio
		for(var i = 0; i < nodes.Length; i++){
			var node = new Node (); // Cria um node que vai dentro do array
			node.label = i.ToString (); // Converte o valor de i para string
			nodes[i] = node; // relaciona os nodes atuais de i para um espaço dentro do array
		}

		// Iteração através de cada uma das linhas no grid
		for(var r = 0; r < rows; r++){
			for (var c = 0; c < cols; c++) { // passa por cada uma das colunas
				var node = nodes[cols*r + c]; // obtêm uma referência a um node dentro do array Nodes. Convertendo as linhas e as colunas no espaço correto dentro do array
				
				// O valor da grade é de uma wall? 0 = "open tile", 1 = "solid tile".
				if(grid[r,c] == 1){
					continue;
				}


				// Conectando um node para os outros

				// UP
				if(r > 0){
					node.adjecent.Add(nodes[cols*(r-1)+c]);
				}

				// RIGHT
				if(c < cols-1){
					node.adjecent.Add(nodes[cols*r+c+1]);
				}

				// DOWN
				if(r < rows - 1) {
					node.adjecent.Add(nodes[cols*(r+1)+c]);
				}

				// LEFT
				if(c > 0){
					node.adjecent.Add(nodes[cols*r+c-1]);
				}
			}
		}


	}

}
