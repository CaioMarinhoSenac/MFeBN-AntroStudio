using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRangedAim : MonoBehaviour
{
    [Header("Configura��es da mira do Inimigo")]
    private GameObject target;
    [Space]
    [Header("Referencias de cena")]
    [SerializeField] Transform enemyTransform;
    [SerializeField] SpriteRenderer rendererEnemy;
    [SerializeField] float distance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Aiming();
    }
    void Aiming()
    {
            // Calcula a dire��o do inimigo para o jogador
            var directionToTarget = target.transform.position - enemyTransform.position;
            directionToTarget.z = 0;

            // Calcula o �ngulo entre a dire��o atual e a dire��o para o jogador
            var angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;   
            
            // Aplica a rota��o para que o inimigo aponte para o jogador
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.position = enemyTransform.position + (distance * directionToTarget.normalized);

            // FLIP
            Vector3 scale = Vector3.one;

            if (angle > 90 || angle < -90)
            {
                scale.y = -1f;
            }
            else
            {
                scale.y = 1f;
            }

            transform.localScale = scale;

            if (angle > 160 || angle < 20)
            {
                // Define a ordem padr�o da camada
                rendererEnemy.sortingOrder = 4;
            }
            else
            {
                rendererEnemy.sortingOrder = 6;
            }
    }
}
