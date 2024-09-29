using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    private float tempoDeDuracao;
    private float velocidade;
    private float dano;
    [SerializeField] protected GameObject collisionFX;

    public void ConfigurarProjetil(float dano, float velocidade, float tempoDeDuracao)
    {
        this.dano = dano;
        this.velocidade = velocidade;
        this.tempoDeDuracao = tempoDeDuracao;
    }
    protected void Start()
    {
        // Destruir após um tempo
        Destroy(gameObject, tempoDeDuracao);
    }
    protected void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InimigoCorpo"))
        {
            EnemyLifeScript enemyLifeScript = collision.GetComponentInParent<EnemyLifeScript>();
            if (enemyLifeScript != null)
            {
                enemyLifeScript.ReceberDano(dano);
                Instantiate(collisionFX, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Parede"))
        {
            Instantiate(collisionFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
