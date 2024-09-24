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

    private void Update()
    {
        if (!Player.vivo)
        {
            Player.ReloadPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
