using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPool : MonoBehaviour
{
    public GameObject shellAutomaticaPrefab;  
    public GameObject shellEscopetaPrefab; 
    public GameObject shellInimigaPrefab; 
    public int poolSize;   
    public AudioSource somShell;

    private Dictionary<int, Queue<GameObject>> shellPools;

    void Start()
    {
        shellPools = new Dictionary<int, Queue<GameObject>>();

        // Inicializa o pool do player (0)
        shellPools[0] = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellAutomaticaPrefab);
            shell.SetActive(false);
            shellPools[0].Enqueue(shell);  
        }

        // Inicializa o pool do inimigo (1)
        shellPools[1] = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellInimigaPrefab);
            shell.SetActive(false);
            shellPools[1].Enqueue(shell);  // Adiciona ao Queue
        }

        // Inicializa o pool da shorty (2)
        shellPools[2] = new Queue<GameObject>(); 
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shell = Instantiate(shellEscopetaPrefab);
            shell.SetActive(false);
            shellPools[2].Enqueue(shell); 
        }
    }

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
            GameObject oldestShell = selectedPool.Dequeue();
            oldestShell.SetActive(false);  
            selectedPool.Enqueue(oldestShell);  
            return oldestShell;
        }

        
        GameObject newShell = shellType == 0 ? Instantiate(shellAutomaticaPrefab) :
                             shellType == 1 ? Instantiate(shellInimigaPrefab) :
                             Instantiate(shellEscopetaPrefab);
        newShell.SetActive(false);
        selectedPool.Enqueue(newShell);
        return newShell;
    }
    private IEnumerator SomDoShell()
    {
        yield return new WaitForSeconds(0.35f);
        somShell.Play();
    }
}
