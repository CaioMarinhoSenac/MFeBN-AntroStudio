using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmaAutomatica : MonoBehaviour
{
    [Header("Configurar arma:")]
    [SerializeField] protected float cadencia;
    [SerializeField] protected int municaoMaxima;    
    [Space]
    [Header("Referencias")]
    public GameObject projetil;
    public GameObject muzzleFlash;
    public GameObject RecarregarSlider;
    public BulletShellEjector bulletShellEjector;
    [SerializeField] protected Transform[] canos;
    [SerializeField] protected Animator animator;
    [SerializeField] protected CamAnimations camAnimations;

    protected int cano;
    protected bool recarregando;
    protected int municaoAtual;
    protected float cadenciaControl;
    protected Vector3 playerParaDirecao;
    protected float movimentSpeed;
    protected float tempoDeRecarga;

    protected void Start()
    {
        municaoAtual = municaoMaxima;
        tempoDeRecarga = 1.5f;
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
        if (cano >= canos.Length)
        {
            cano = 0;
            yield break;
        }
        else
        {
            cadenciaControl = Time.time + cadencia;
            municaoAtual--;

            Instantiate(projetil, canos[cano].position, canos[cano].rotation);
            bulletShellEjector.EjectShell();

            camAnimations.CamShake();            

            cano++;

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
    protected bool PodeDisparar()
    {
        return Time.time > cadenciaControl;
    }
    protected virtual IEnumerator Recarregar()
    {
        municaoAtual = 0;
        muzzleFlash.SetActive(false);
        recarregando = true;
        animator.SetBool("Recarregando", true);

        RecarregarSlider.SetActive(true);        

        yield return new WaitForSeconds(tempoDeRecarga);
        recarregando = false;
        animator.SetBool("Recarregando", false);

        municaoAtual = municaoMaxima;
        RecarregarSlider.SetActive(false);
    }
}
