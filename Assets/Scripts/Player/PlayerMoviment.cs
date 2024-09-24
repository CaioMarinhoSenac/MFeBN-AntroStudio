using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMoviment : MonoBehaviour
{
    private Vector2 movimentDirection;
    private float movimentSpeed;

    [SerializeField] private float dynamicMovimentSpeed = 1f;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (Player.podeAndar == true)
        {
            Inputs();
            Move();
            Animate();
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
}