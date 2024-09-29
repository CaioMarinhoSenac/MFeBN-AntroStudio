using System.Collections;
using UnityEngine;

public class ArmaDeFogoInimiga : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected float distanciaParaDisparar;
    [SerializeField] protected Transform cano;
    [SerializeField] protected float spawnCooldown;
    [SerializeField] protected float danoDoProjetil;
    [SerializeField] protected float velocidadeDoProjetil;
    [SerializeField] protected float duracaoDoProjetil;
    [Space]
    [Header("Referencias de Cena")]
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected Transform ejectionPoint;
    [SerializeField] protected int tipoShell;
    [SerializeField] protected ShellPool shellPool;
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
    protected void EjetarCartucho()
    {
        if (shellPool != null)
        {
            // Obtém o cartucho do pool
            GameObject shell = shellPool.GetShell(tipoShell);

            // Gera um ponto aleatório ao redor do ejectionPoint
            Vector2 randomPosition = Random.insideUnitCircle * 0.3f;
            Vector3 shellPosition = ejectionPoint.position + new Vector3(randomPosition.x, randomPosition.y, 0);

            float randomRotation = Random.Range(-15f, 15f);
            Quaternion shellRotation = ejectionPoint.rotation * Quaternion.Euler(0, 0, randomRotation);

            shell.transform.SetPositionAndRotation(shellPosition, shellRotation);
            shell.SetActive(true);  // Ativa o cartucho

            // Adiciona força e torque para simular a ejeção
            Rigidbody2D shellRb = shell.GetComponent<Rigidbody2D>();
            if (shellRb != null)
            {
                Vector2 ejectionDirection = ejectionPoint.up;
                shellRb.AddForce(ejectionDirection * 0.5f, ForceMode2D.Impulse);
                shellRb.AddTorque(Random.Range(-10f, 0.5f));
            }
        }
    }
}
