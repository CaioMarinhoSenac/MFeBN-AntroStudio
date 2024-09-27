using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPool : MonoBehaviour
{
    public GameObject shellPistolaPrefab;  // Prefab do cartucho da pistola
    public GameObject shellShortyPrefab;    // Prefab do cartucho da shorty
    public GameObject shellEnemyPrefab;      // Prefab do cartucho do inimigo
    public int poolSize;                     // Tamanho do pool para cada tipo de cartucho
    public AudioSource somShell;

    private Dictionary<int, Queue<GameObject>> shellPools; // Dicionário para os pools como fila (Queue)

    void Start()
    {
        // Inicializa o dicionário de filas (Queue)
        shellPools = new Dictionary<int, Queue<GameObject>>();

        // Inicializa o pool do jogador (0) para a pistola
        shellPools[0] = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellPistolaPrefab);
            shell.SetActive(false);
            shellPools[0].Enqueue(shell);  // Adiciona ao Queue
        }

        // Inicializa o pool do inimigo (1)
        shellPools[1] = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellEnemyPrefab);
            shell.SetActive(false);
            shellPools[1].Enqueue(shell);  // Adiciona ao Queue
        }

        // Inicializa o pool da shorty (2)
        shellPools[2] = new Queue<GameObject>();  // Corrigido: use 2 para shorty
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellShortyPrefab);
            shell.SetActive(false);
            shellPools[2].Enqueue(shell);  // Adiciona ao Queue
        }
    }

    // Método para obter um cartucho com base no tipo (0 = pistola, 1 = inimigo, 2 = shorty)
    public GameObject GetShell(int shellType)
    {
        if (!shellPools.ContainsKey(shellType))
        {
            return null;
        }

        Queue<GameObject> selectedPool = shellPools[shellType];

        StartCoroutine(SomDoShell());

        // Verifica se há shells inativos na fila
        if (selectedPool.Count > 0)
        {
            foreach (GameObject shell in selectedPool)
            {
                if (!shell.activeInHierarchy)
                {
                    return shell;  // Retorna o cartucho inativo
                }
            }
        }

        // Se todos os cartuchos estiverem ativos e o limite do pool for atingido:
        if (selectedPool.Count >= poolSize)
        {
            // Remove o shell mais antigo da fila, desativa e reutiliza
            GameObject oldestShell = selectedPool.Dequeue();
            oldestShell.SetActive(false);  // Desativa o shell mais antigo
            selectedPool.Enqueue(oldestShell);  // Reinsere o shell no final da fila
            return oldestShell;  // Reutiliza o shell mais antigo
        }

        // Se o pool não estiver cheio, cria um novo cartucho e adiciona ao pool
        GameObject newShell = shellType == 0 ? Instantiate(shellPistolaPrefab) :
                             shellType == 1 ? Instantiate(shellEnemyPrefab) :
                             Instantiate(shellShortyPrefab);
        newShell.SetActive(false);  // Desativa o novo cartucho
        selectedPool.Enqueue(newShell);  // Adiciona o novo cartucho ao pool
        return newShell;
    }
    private IEnumerator SomDoShell()
    {
        yield return new WaitForSeconds(0.3f);
        somShell.Play();
    }
}
