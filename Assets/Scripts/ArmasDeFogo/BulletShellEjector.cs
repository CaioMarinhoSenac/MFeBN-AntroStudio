using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellEjector : MonoBehaviour
{
    [Header("Referências:")]
    public ShellPool shellPool; // Referência ao pool de cartuchos
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
    }

    public void EjectShell()
    {
        // Obtém o cartucho do pool
        GameObject shell = shellPool.GetShell(shellType);

        // Gera um ponto aleatório ao redor do ejectionPoint
        Vector2 randomPosition = Random.insideUnitCircle * 0.5f;
        Vector3 shellPosition = ejectionPoint.position + new Vector3(randomPosition.x, randomPosition.y, 0);

        float randomRotation = Random.Range(-15f, 15f);
        Quaternion shellRotation = ejectionPoint.rotation * Quaternion.Euler(0, 0, randomRotation);

        shell.transform.SetPositionAndRotation(shellPosition, shellRotation);
        shell.SetActive(true); // Ativa o cartucho

        // Adiciona força e torque para simular a ejeção
        Rigidbody2D shellRb = shell.GetComponent<Rigidbody2D>();
        if (shellRb != null)
        {
            Vector2 ejectionDirection = ejectionPoint.up;
            shellRb.AddForce(ejectionDirection * ejectionForce, ForceMode2D.Impulse);
            shellRb.AddTorque(Random.Range(-torqueForce, torqueForce));
        }
    }
}
