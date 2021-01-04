using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public List <Node> adjecent = new List<Node>(); // outros nodes adjacentes ao node em que estamos
	public Node previous = null; // O node anterior que nós olhamos
	public string label = ""; // Cada node vai ter um nome diferente
	public Transform NodePos;

	public void Clear (){ // limpando nosso node para que possamos redefinir o campo anterior
		previous = null;
	}

}
