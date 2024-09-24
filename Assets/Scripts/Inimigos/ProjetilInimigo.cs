using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    [SerializeField] protected float tempoDeDuracao;
    [SerializeField] protected float velocidade;
    [SerializeField] protected GameObject collisionFX;

    protected void Start()
    {
        // Destruir após um tempo
        Destroy(gameObject, tempoDeDuracao);
    }
    protected void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCorpo"))
        {
            VidaScript.ReceberDano(0.5f);
            Instantiate(collisionFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Parede"))
        {
            Instantiate(collisionFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
