using System.Collections;
using UnityEngine;
using System;
using System.Data;
using System.Collections.Generic;
using UnityEngine.UI;

public class highScoreManager : MonoBehaviour {

	private string connectionString;
	private List <highscores> highscore = new List<highscores> (); // Lista de highscores
	public Transform scoreParent; // set parent para adicionar scores 
	public GameObject nameDialog;// Caixa de texto onde o jogador coloca o nome depois de completar o jogo.
	public InputField enterName; // Nome do jogador


	void Start () {
		connectionString = "URI= file:" + Application.dataPath + "/highh.sqlite"; // Conecta o Unity com o SQLite que foi feito para o sqlite manager

		CreateTable ();// "CreateTable" se não estiver no arquivo .sqlite
        ShowScore (); // Mostra o Score
	}
		
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			nameDialog.SetActive(!nameDialog.activeSelf); // Botão Espaço para abrir a caixa de diálogo para colocar o nome.
		}
	}
		
	public void EnterName() // Método para colocar o nome
	{
			int score = UnityEngine.Random.Range (10000, 50000);
			InsertScore (enterName.text, score);  // Chama o método para inserir o nome e o Score
		    enterName.text = string.Empty;
			ShowScore ();
	}

	private void InsertScore(string name, int newScore)  // Métodos para inserir o score
	{

	}

	private void GetScore() // Métodos para pegar o score
	{

	}

	private void CreateTable() // Tabela para o Highscore se não existir
	{

	}
	private void ShowScore () // Método para mostrar o score
	{
		GetScore ();
		Debug.Log ("Verificando contagem do score " + highscore.Count);

		Text tmp = GameObject.FindGameObjectWithTag ("Rank").GetComponent<Text> () as Text; // Procura um texto que tenha a tag rank
		Text forName = GameObject.FindGameObjectWithTag ("naming").GetComponent<Text> () as Text; // Procura um texto que tenha a tag naming
        Text forScore= GameObject.FindGameObjectWithTag ("Score").GetComponent<Text> () as Text; // Procura um texto que tenha a tag score

        tmp.text = "";
		forName.text = "";
		forScore.text = "";
		int i = 0;

		foreach (highscores h in highscore) {
			Debug.Log ("Verificando score " + h.Name + " " + h.Score + " ::: " + h.ID);// Verifica se funciona ou não

			 tmp.text += "#" + (i++ + 1)+ "\n"; //Adiciona rank
			forName.text += h.Name +"\n"; //Adiciona nome
			forScore.text += h.Score +"\n"; //Adiciona score			
			}	
		}
	}


