using System.Collections;
using UnityEngine;

public class ArmaDeFogo : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected int municaoMaxima;
    [SerializeField] protected float tempoDeRecarga;
    [SerializeField] protected float danoDoProjetil;
    [SerializeField] protected float velocidadeDoProjetil;
    [SerializeField] protected float duracaoDoProjetil;
    [Space]
    [Header("Referencias")]
    [SerializeField] protected ShellPool shellPool;
    [SerializeField] protected Transform ejectionPoint;
    [SerializeField] protected int tipoShell;
    [SerializeField] protected GameObject projetil;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected GameObject RecarregarSlider;
    [SerializeField] protected Transform[] canos;
    [SerializeField] protected Animator animator;
    [SerializeField] protected CamAnimations camAnimations;
    [SerializeField] protected AudioSource somDisparo;
    [SerializeField] protected AudioSource somRecarga;


    protected int cano;
    protected bool recarregando;
    protected int municaoAtual;
    protected float cadenciaControl;
    protected Vector3 playerParaDirecao;
    protected float movimentSpeed;

    protected void Start()
    {
        municaoAtual = municaoMaxima;
        recarregando = false;
        cano = 0;
        camAnimations = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamAnimations>();
        muzzleFlash.SetActive(false);
    }
    protected void OnEnable()
    {
        recarregando = false;
        muzzleFlash.SetActive(false);
        animator.SetBool("Recarregando", false);
    }
    protected void Update()
    {
        // ANIMAÇÃO
        playerParaDirecao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerParaDirecao.z = 0;
        movimentSpeed = Mathf.Clamp(playerParaDirecao.magnitude, 0f, 1f);

        if (playerParaDirecao != Vector3.zero)
        {
            animator.SetFloat("Horizontal", playerParaDirecao.x);
            animator.SetFloat("Vertical", playerParaDirecao.y);
        }

        animator.SetFloat("Speed", movimentSpeed);

        // DISPARO
        if (recarregando)
        {
            return;
        }
        if (municaoAtual <= 0 || (municaoAtual < municaoMaxima && Input.GetKeyDown(KeyCode.R)))
        {
            StartCoroutine(Recarregar());
        }
        if (Input.GetMouseButton(0) && PodeDisparar() && municaoAtual > 0)
        {
            StartCoroutine(Disparar());
        }
    }
    protected virtual IEnumerator Disparar()
    {
        //
        // PARTE CUSTOMIZAVEL PRINCIPAL.
        //

        yield break;
    }
    protected bool PodeDisparar()
    {
        return Time.time > cadenciaControl;
    }
    protected IEnumerator Recarregar()
    {
        municaoAtual = 0;
        muzzleFlash.SetActive(false);
        recarregando = true;
        animator.SetBool("Recarregando", true);

        somRecarga.Play();

        RecarregarSlider.SetActive(true);

        yield return new WaitForSeconds(tempoDeRecarga);
        recarregando = false;
        animator.SetBool("Recarregando", false);

        municaoAtual = municaoMaxima;
        RecarregarSlider.SetActive(false);
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
