using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyWeapon : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected float distanciaParaDisparar;
    [SerializeField] protected Transform cano;
    [SerializeField] protected GameObject muzzleFlash;
    [Space]
    [Header("Referencias de Cena")]
    private GameObject target;
    [SerializeField] private GameObject projetilInimigo;
    [SerializeField] private BulletShellEjector bulletShellEjector;
    [SerializeField] protected Animator animator;
    [SerializeField] private AudioSource somDisparo;

    protected float cadenciaControl;
    protected float movimentSpeed;
    private bool podeAtirar;

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
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.z = 0;

        movimentSpeed = Mathf.Clamp(directionToTarget.magnitude, 0f, 1f);

        // Calcula o ângulo entre a direção atual e a direção para o jogador
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        if (directionToTarget != Vector3.zero)
        {
            animator.SetFloat("Horizontal", directionToTarget.x);
            animator.SetFloat("Vertical", directionToTarget.y);
        }

        animator.SetFloat("Speed", movimentSpeed);

        // Aplica a rotação para que o inimigo aponte para o jogador
        transform.eulerAngles = new Vector3(0, 0, angle);

        if ((Vector2.Distance(target.transform.position, transform.position) <= distanciaParaDisparar) && DispararCooldown() && Player.vivo && podeAtirar)
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
        else {
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
