using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedMoviment : MonoBehaviour
{
    [Header("Configurações da movimentação do Inimigo")]
    [SerializeField] private float dynamicMovimentSpeed = 1f;
    [SerializeField] private Animator animator;   
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [Space]
    [Header("Referencias de cena")]
    private GameObject target;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private AudioSource somPassos;

    private Vector2 movimentDirection;
    private float movimentSpeed;

    private bool podeTocarPasso = true;
    private float cooldownPassos = 0.1f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 directionToPlayer = target.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > maxDistance)
        {
            // Avança
            movimentDirection = directionToPlayer.normalized;
        }
        else if (distanceToPlayer <= minDistance)
        {
            // Para ou recua
            if (distanceToPlayer <= minDistance + 0.1f && distanceToPlayer >= minDistance - 0.1f)
            {
                // Para
                movimentDirection = Vector2.zero;
            }
            else
            {
                // Recua
                movimentDirection = -directionToPlayer.normalized;
            }
        }

        movimentSpeed = Mathf.Clamp(movimentDirection.magnitude, 0f, 1f);

        Move();
        Animate();
    }

    void Move()
    {
        rigidBody.velocity = dynamicMovimentSpeed * movimentSpeed * movimentDirection;
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
