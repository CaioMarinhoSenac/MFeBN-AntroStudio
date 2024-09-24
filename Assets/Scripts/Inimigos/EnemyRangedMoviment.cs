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

    private Vector2 movimentDirection;
    private float movimentSpeed;

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
}
