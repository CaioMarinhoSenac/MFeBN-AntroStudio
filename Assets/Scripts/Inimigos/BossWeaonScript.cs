using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossWeaonScript : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected Transform cano;
    [SerializeField] protected GameObject muzzleFlash;
    [Space]
    [Header("Referencias de Cena")]   
    [SerializeField] private GameObject projetilInimigo;
    [SerializeField] private BulletShellEjector bulletShellEjector;
    [SerializeField] protected Animator animator;
    [SerializeField] private AudioSource somDisparo;
    [SerializeField] protected GameObject boss;
    [SerializeField] private SpriteRenderer rendererBoss;

    protected float cadenciaControl;
    private bool podeAtirar;
    private GameObject target;

    private void Start()
    {
        podeAtirar = false;
        StartCoroutine(Spawnou());
        muzzleFlash.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Calcula a direção do inimigo para o jogador
        Vector3 directionToTarget = target.transform.position - boss.transform.position;
        directionToTarget.z = 0;

        // Calcula o ângulo entre a direção atual e a direção para o jogador
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        if (directionToTarget != Vector3.zero)
        {
            animator.SetFloat("Horizontal", directionToTarget.x);
            animator.SetFloat("Vertical", directionToTarget.y);
        }

        // Aplica a rotação para que o inimigo aponte para o jogador
        boss.transform.eulerAngles = new Vector3(0, 0, angle);

        // FLIP
        Vector3 escala = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            escala.y = -1f;
        }
        else
        {
            escala.y = 1f;
        }

        transform.localScale = escala;

        // ** PLAYER **      
        // ORDER IN LAYER
        if (angle > 160 || angle < 20)
        {
            // Define a ordem padrão da camada
            rendererBoss.sortingOrder = 4;
        }
        else
        {
            rendererBoss.sortingOrder = 6;
        }

        if (DispararCooldown() && Player.vivo && podeAtirar)
        {
            StartCoroutine(Disparar());
        }
    }

    protected IEnumerator Disparar()
    {
        if (!DispararCooldown())
        {
            yield break;
        }
        else
        {
            cadenciaControl = Time.time + cadencia;

            Instantiate(projetilInimigo, cano.position, cano.rotation);
            bulletShellEjector.EjectShell();
            somDisparo.Play();

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
    protected bool DispararCooldown()
    {
        return Time.time > cadenciaControl;
    }
    protected IEnumerator Spawnou()
    {
        yield return new WaitForSeconds(1f);
        podeAtirar = true;
    }
}
