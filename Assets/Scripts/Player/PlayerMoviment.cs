using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Vector2 movimentDirection;
    private float movimentSpeed;
    private Vector2 mouseDirection;
    private bool canDash = true;
    private bool dashing = false;

    [Header("Configurar PlayerMoviment:")]
    [SerializeField] private float dynamicMovimentSpeed = 1f;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource somPassos;
    [Header("Configurar Dash:")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashCooldown;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashFXDuration;
    [SerializeField] public AudioSource somDash;

    private float cooldownPassos = 0.1f;

    private bool podeTocarPasso = true;

    void Update()
    {
        if (Player.podeAndar == true)
        {
            Inputs();
            Move();
            AplicarDash();
            Animate();         
        }
        if (dashing)
        {
            Shadows.instance.SombrasSkill();
        }
    }

    void Inputs()
    {
        movimentDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movimentSpeed = Mathf.Clamp(movimentDirection.magnitude, 0f, 1f);
        movimentDirection.Normalize();
    }

    void Move()
    {
        rigidBody.velocity = movimentDirection * movimentSpeed * dynamicMovimentSpeed;
    }

    void Animate()
    {
        if (movimentDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movimentDirection.x);
            animator.SetFloat("Vertical", movimentDirection.y);
        }

        animator.SetFloat("Speed", movimentSpeed);
    }

    void AplicarDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        dashing = true;
        Player.animator.SetBool("Dash", true);
        Player.podeAndar = false;
        VidaScript.AtivarInvencibilidade(dashFXDuration);

        somDash.Play();

        float elapsedTime = 0f;
        Vector2 originalVelocity = rigidBody.velocity;

        while (elapsedTime < dashDuration)
        {
            rigidBody.velocity = movimentDirection * dashSpeed * Time.fixedDeltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rigidBody.velocity = originalVelocity;

        Player.podeAndar = true;

        StartCoroutine(DashCooldown());

        yield return new WaitForSeconds(dashFXDuration);
        Player.animator.SetBool("Dash", false);
        dashing = false;
    }

    IEnumerator DashCooldown()
    {
        canDash = false; 
        yield return new WaitForSeconds(dashCooldown); 
        canDash = true; 
    }

    public void Passos()
    {
        if (podeTocarPasso)
        {
            somPassos.Play();
            StartCoroutine(CooldownPassos());  // Inicia o cooldown após tocar o som
        }
    }
    private IEnumerator CooldownPassos()
    {
        podeTocarPasso = false;  // Impede que o som seja tocado novamente
        yield return new WaitForSeconds(cooldownPassos);  // Espera pelo tempo de cooldown
        podeTocarPasso = true;  // Permite que o som possa ser tocado novamente
    }
}