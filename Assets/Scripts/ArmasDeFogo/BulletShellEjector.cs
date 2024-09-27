using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellEjector : MonoBehaviour
{
    [Header("Refer�ncias:")]
    public ShellPool shellPool; // Refer�ncia ao pool de cartuchos
    public Transform ejectionPoint;
    [Header("Configurar:")]
    public int shellType;

    private float ejectionForce = 0.5f;
    private float torqueForce = 10f;

    void Start()
    {
        if (shellPool == null)
        {
            shellPool = FindObjectOfType<ShellPool>();
        }

        if (CompareTag("Player"))
        {
            shellType = 0; // Jogador
        }
        else if (CompareTag("Enemy"))
        {
            shellType = 1; // Inimigo
        }
    }

    public void EjectShell()
    {
        // Obt�m o cartucho do pool
        GameObject shell = shellPool.GetShell(shellType);
        shell.transform.position = ejectionPoint.position;
        shell.transform.rotation = ejectionPoint.rotation;
        shell.SetActive(true); // Ativa o cartucho

        // Adiciona for�a e torque para simular a eje��o
        Rigidbody2D shellRb = shell.GetComponent<Rigidbody2D>();
        if (shellRb != null)
        {
            Vector2 ejectionDirection = ejectionPoint.up;
            shellRb.AddForce(ejectionDirection * ejectionForce, ForceMode2D.Impulse);
            shellRb.AddTorque(Random.Range(-torqueForce, torqueForce));
        }
    }
}
