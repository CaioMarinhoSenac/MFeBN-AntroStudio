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
    [SerializeField] protected Animator animator;

    protected float cadenciaControl;
    protected float movimentSpeed;

    private void Start()
    {
        muzzleFlash.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Spawnou());
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

        if ((Vector2.Distance(target.transform.position, transform.position) <= distanciaParaDisparar) && PodeDisparar() && Player.vivo)
        {
            StartCoroutine(Disparar());
        }
    }

    protected IEnumerator Disparar()
    {
        if (!PodeDisparar())
        {
            yield break;
        }
        else {
            cadenciaControl = Time.time + cadencia;
            Instantiate(projetilInimigo, cano.position, cano.rotation);

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
    protected bool PodeDisparar()
    {
        return Time.time > cadenciaControl;
    }
    protected IEnumerator Spawnou()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
