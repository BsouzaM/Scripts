using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitLevel : MonoBehaviour
{
    public void Quitting()
    {
        Application.Quit();
        Debug.Log("Player saiu do jogo!");
    }
}