using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search {

	public Graph graph;
	public List<Node> reachable; // nodes alcançáveis
	public List<Node> explored; // nodes explorados
	public List<Node> path;

	public Node goalNode;
	public int iterations; // Numero de iterações que a pesquisa executa
	public bool finished; // Nossa pesquisa foi completada?

	public Search(Graph graph) {
		this.graph = graph;
	}

	public void Start(Node start, Node goal) {
		reachable = new List<Node>(); 
		reachable.Add (start); // O node de partida é sempre acessível

		goalNode = goal;

		explored = new List<Node> ();
		path = new List<Node> ();
		iterations = 0;

		for (var i = 0; i < graph.nodes.Length; i++) {
			graph.nodes [i].Clear (); // Reseta todos os valores da pesquisa
		}

	}

	public void Step() {
		if (path.Count > 0) { // para parar esse método if, precisamos encontrar um caminho
			return;
		}

		if (reachable.Count == 0) { // nesse caso, não encontramos uma solução, então a pesquisa está concluída
			finished = true;
			return;
		}

		iterations++;

		var node = ChoseNode (); // retorna um node aleatório dos nodes alcançáveis
		if (node == goalNode) { // verifica se o node é realmente igual ao node de objetivo
			while(node != null){ // Preenche o caminho para a forma como chegamos aqui.
				path.Insert (0, node);
				node = node.previous;
			}

			finished = true;
			return;
		}

		reachable.Remove (node); // Isso acontece se não chegamos ao final da pesquisa
		explored.Add (node); // adicionando o node à lista explorada

		// itera através de todos os nodes adjacentes anexados ao node atual
		for(var i = 0; i < node.adjecent.Count; i++) {
			AddAdjecent (node, node.adjecent [i]); // adicione o node atual e seus próprios nodes adjacentes
		}
	}

	public void AddAdjecent (Node node, Node adjecent){
		if(FindNode(adjecent, explored) || FindNode(adjecent, reachable)){
			return;
		}
		
		// Se o if-statement não retornar true, significa que nós achamos um novo path
		adjecent.previous = node;
		reachable.Add (adjecent);
		
	}

	public bool FindNode (Node Node, List<Node> list) {
		return GetNodeIndex(Node, list) >= 0; // retorna verdadeiro se o node existir dentro da lista, se não é falso.
	}

	public int GetNodeIndex(Node node, List<Node> list){
		for (var i = 0; i < list.Count; i++) { // O node existe dentro da lista?
			if(node == list[i]){
				return i;
			}
		}

		return -1;
	}

	public Node ChoseNode(){ // retorna um node aleatório dos nodes alcançáveis
		return reachable [Random.Range (0, reachable.Count)];
	}
}
