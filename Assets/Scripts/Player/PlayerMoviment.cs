using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

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
    [Header("Configurar Dash:")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashCooldown;
    [SerializeField] public float dashDuration;
    [SerializeField] public float DashFXduration;

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
        VidaScript.Invencivel(DashFXduration);

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

        yield return new WaitForSeconds(DashFXduration);
        Player.animator.SetBool("Dash", false);
        dashing = false;
    }

    IEnumerator DashCooldown()
    {
        canDash = false; 
        yield return new WaitForSeconds(dashCooldown); 
        canDash = true; 
    }
}