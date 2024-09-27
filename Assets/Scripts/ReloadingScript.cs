using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingScript : MonoBehaviour
{
    private float tempoDeDuracao, tempo;
    public Image fill;
    public AudioSource somRecarregar;

    private void OnEnable()
    {
        somRecarregar.Play();
        tempoDeDuracao = 1.5f;
        tempo = tempoDeDuracao;
        Update();        
    }
    void Update()
    {
        tempo -= Time.deltaTime;
        fill.fillAmount = tempo / tempoDeDuracao;

        if (tempo < 0)
        {
            tempo = 0;
        }
    }
}
