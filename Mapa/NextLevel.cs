using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Player entrou no jogo!");  
    }

    /*
     * public int num;
     * 
     * void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(num, LoadSceneMode.Single);
            Debug.Log("Próxima Scene: " + num);
        }
    }*/
}