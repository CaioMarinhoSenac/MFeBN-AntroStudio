using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossLifeScript : MonoBehaviour
{
    [HideInInspector] public float vida;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform transformBoss;

    public void ReceberDano(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        animator.SetBool("Morrer", true);

        Destroy(GetComponent<PolygonCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Light2D>());
    }

    public class CoroutineManagerEnemy : MonoBehaviour
    {
        private static CoroutineManagerEnemy _instance;

        public static CoroutineManagerEnemy Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("CoroutineManagerEnemy");
                    _instance = gameObject.AddComponent<CoroutineManagerEnemy>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            _instance = this;
        }

    }
}
