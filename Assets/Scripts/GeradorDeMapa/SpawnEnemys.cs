using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPointsParent;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        FindSpawnPoints();
        SpawnEnemies();
    }

    // Função para encontrar todos os SpawnPoints filhos do objeto pai
    void FindSpawnPoints()
    {
        foreach (Transform child in spawnPointsParent)
        {
            spawnPoints.Add(child);
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Instancia um inimigo em cada SpawnPoint
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
    }
}
