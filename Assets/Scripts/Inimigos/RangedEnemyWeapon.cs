using System.Collections;
using UnityEngine;

public class RangedEnemyWeapon : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected float distanciaParaDisparar;
    [SerializeField] protected Transform cano;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected float spawnCooldown;
    [Space]
    [Header("Referencias de Cena")]
    [SerializeField] protected GameObject projetilInimigo;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource somDisparo;
    [SerializeField] protected AudioSource somRecarga;

    protected float cadenciaControl;
    protected float movimentSpeed;
    protected bool podeAtirar;
    protected GameObject target;

    protected void Start()
    {
        podeAtirar = false;
        StartCoroutine(Spawnou());
        muzzleFlash.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        // Calcula a dire��o do inimigo para o jogador
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.z = 0;

        movimentSpeed = Mathf.Clamp(directionToTarget.magnitude, 0f, 1f);

        // Calcula o �ngulo entre a dire��o atual e a dire��o para o jogador
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        if (directionToTarget != Vector3.zero)
        {
            animator.SetFloat("Horizontal", directionToTarget.x);
            animator.SetFloat("Vertical", directionToTarget.y);
        }

        animator.SetFloat("Speed", movimentSpeed);

        // Aplica a rota��o para que o inimigo aponte para o jogador
        transform.eulerAngles = new Vector3(0, 0, angle);

        if ((Vector2.Distance(target.transform.position, transform.position) <= distanciaParaDisparar) && PodeDisparar() && Player.vivo && podeAtirar)
        {
            StartCoroutine(Disparar());
        }
    }

    protected virtual IEnumerator Disparar()
    {

        //
        // PARTE CUSTOMIZAVEL PRINCIPAL
        //

        yield break;
    }
    protected bool PodeDisparar()
    {
        return Time.time > cadenciaControl;
    }
    protected IEnumerator Spawnou()
    {
        yield return new WaitForSeconds(spawnCooldown);
        podeAtirar = true;
    }
}
