using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComandsManager : MonoBehaviour
{
    public static void Jogar()
    {
        SceneManager.LoadScene(1);
    }
    public static void Sair()
    {
        Application.Quit();
    }
}
