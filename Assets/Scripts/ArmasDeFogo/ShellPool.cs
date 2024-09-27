using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPool : MonoBehaviour
{
    public GameObject shellPlayerPrefab;  // Prefab do cartucho do jogador
    public GameObject shellEnemyPrefab;   // Prefab do cartucho do inimigo
    public int poolSize;  // Tamanho do pool para cada tipo de cartucho

    private Dictionary<int, List<GameObject>> shellPools; // Dicionário para os pools

    void Start()
    {
        // Inicializa o dicionário
        shellPools = new Dictionary<int, List<GameObject>>();

        // Inicializa o pool do jogador (0)
        shellPools[0] = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellPlayerPrefab);
            shell.SetActive(false);
            shellPools[0].Add(shell);
        }

        // Inicializa o pool do inimigo (1)
        shellPools[1] = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellEnemyPrefab);
            shell.SetActive(false);
            shellPools[1].Add(shell);
        }
    }

    // Método para obter um cartucho com base no tipo (0 = jogador, 1 = inimigo)
    public GameObject GetShell(int shellType)
    {
        if (!shellPools.ContainsKey(shellType))
        {
            Debug.LogError("Tipo de shell inválido: " + shellType);
            return null;
        }

        List<GameObject> selectedPool = shellPools[shellType];

        foreach (GameObject shell in selectedPool)
        {
            if (!shell.activeInHierarchy)
            {
                return shell;  // Retorna o cartucho inativo
            }
        }

        // Se todos os cartuchos estiverem ativos, cria um novo e o adiciona ao pool correspondente
        GameObject newShell = shellType == 0 ? Instantiate(shellPlayerPrefab) : Instantiate(shellEnemyPrefab);
        newShell.SetActive(false);  // Desativa o cartucho
        selectedPool.Add(newShell);  // Adiciona ao pool correspondente
        return newShell;
    }
}
