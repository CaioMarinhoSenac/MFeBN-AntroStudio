using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBoss : MonoBehaviour
{
    public float bossVida;
    public Animator bossAnimator;
    public Transform bossTransform;

    private void Start()
    {
        BossLifeScript bossLifeScript = GetComponent<BossLifeScript>();
        bossLifeScript.vida = bossVida;
        bossLifeScript.animator = bossAnimator;
        bossLifeScript.transformBoss = bossTransform;
    }
}
