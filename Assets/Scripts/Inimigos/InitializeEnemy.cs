using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEnemy : MonoBehaviour
{
    public float enemyVida;
    public GameObject enemyMaoArma;
    public Animator enemyAnimator;
    public Transform enemyTransform;

    private void Start()
    {
        EnemyLifeScript enemyLifeScript = GetComponent<EnemyLifeScript>();

        enemyLifeScript.vida = enemyVida;
        enemyLifeScript.animator = enemyAnimator;
        enemyLifeScript.maoArma = enemyMaoArma;
        enemyLifeScript.transformInimigo = enemyTransform;
    }
}
