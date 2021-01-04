using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class highscorescript : MonoBehaviour  {
	
	public GameObject Score;
	public GameObject ScoreName;
	public GameObject Rank;

	public void SetScore(string name, string score, string rank) // Tudo é strings porque vamos inserir tudo no texto.
	{
		Rank.GetComponent<Text>().text = rank;
		ScoreName.GetComponent<Text>().text = name;
		Score.GetComponent<Text>().text = score;
	}
}
