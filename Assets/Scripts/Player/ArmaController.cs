using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    int totalArmas = 1;
    public int armaAtualID;

    public GameObject[] armas;

    public GameObject MaoArma;

    public GameObject armaAtual;

    public GameObject recarregaSlider;

    void Start()
    {
        totalArmas = MaoArma.transform.childCount;
        armas = new GameObject[totalArmas];

        for (int i = 0; i < totalArmas; i++)
        {
            armas[i] = MaoArma.transform.GetChild(i).gameObject;
            armas[i].SetActive(false);
        }

        armas[0].SetActive(true);

        armaAtual = armas[0];
        armaAtualID = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrocarArmas(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrocarArmas(0);
        }

    }

    void TrocarArmas(int armaAtualNova)
    {
        recarregaSlider.SetActive(false);
        armas[armaAtualNova].SetActive(false);

        if (armaAtualNova > 0)
        {
            armaAtualNova--;            
        }
        else
        {
            armaAtualNova++;
        }

        armas[armaAtualNova].SetActive(true);

        armaAtual = armas[armaAtualNova];
    }
}
