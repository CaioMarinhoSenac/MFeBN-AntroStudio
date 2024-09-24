using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControladoraDeSalas : MonoBehaviour
{
    public Transform SalasCena;
    private Sprite anteriorIcon;
    public GameObject Inimigo;

    void Start()
    {
        anteriorIcon = Level.normalRoomIcon;
    }
    void MudarIconeSala(Sala salaAtual, Sala proximaSala)
    {
        salaAtual.SalaImage.sprite = anteriorIcon;

        anteriorIcon = proximaSala.SalaImage.sprite;

        proximaSala.SalaImage.sprite = Level.atualRoomIcon;
    }

    bool mudaSalaCooldown = false;

    void FimMudaSalaCooldown()
    {
        mudaSalaCooldown = false;
    }

    public static void RedesenharSalasReveladas()
    {
        foreach (Sala sala in Level.Salas)
        {
            if (!sala.SalaRevelada && !sala.SalaExplorada)
            {
                sala.SalaImage.color = new Color(1, 1, 1, 0);
            }
            if (sala.SalaRevelada && !sala.SalaExplorada)
            {
                sala.SalaImage.sprite = Level.desconhecidoRoomIcon;
            }
            if (sala.SalaExplorada && sala.ID == 6)
            {
                sala.SalaImage.sprite = Level.normalRoomIcon;
            }
            if (sala.SalaExplorada && sala.ID == 2)
            {
                sala.SalaImage.sprite = Level.lojaRoomIcon;
            }
            if (sala.SalaExplorada && sala.ID == 3)
            {
                sala.SalaImage.sprite = Level.tesouroRoomIcon;
            }

            if (sala.SalaRevelada || sala.SalaExplorada)
            {
                sala.SalaImage.color = new Color(1, 1, 1, 1);
            }
            Player.SalaAtual.SalaImage.sprite = Level.atualRoomIcon;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (mudaSalaCooldown)
        {
            return;
        }
        else
        {
            mudaSalaCooldown = true;
            Invoke(nameof(FimMudaSalaCooldown), Level.cooldownMudaSala);
        }
        
        if (collision.gameObject.CompareTag("EntradaDaEsquerda"))
        {
            Player.podeAndar = false;
            // AONDE O PLAYER ESTÁ
            Vector2 localizacao = Player.SalaAtual.Localizacao;

            // PARA ONDE ELE VAI
            localizacao += new Vector2 (-1, 0);

            if (Level.Salas.Exists(x => x.Localizacao == localizacao))
            {
                Sala sala = Level.Salas.First(x => x.Localizacao == localizacao);

                SalasCena.Find(Player.SalaAtual.ID.ToString()).transform.Find(GeradorDeLevel.DeterminarTipoPrefab(Player.SalaAtual))
                .gameObject.SetActive(false);

                GameObject proximaSala = SalasCena.Find(sala.ID.ToString()).transform.Find
                (GeradorDeLevel.DeterminarTipoPrefab(sala)).gameObject;

                proximaSala.SetActive(true);
                // MOVE O PLAYER PARA A PRÓXIMA ENTRADA

                Player.transform.position = new Vector3(6.2f, 0, 0);

                DestroyGameObjectsWithTag("Enemy");
                DestroyGameObjectsWithTag("Projetil");
                DestroyGameObjectsWithTag("ProjetilInimigo");

                MudarIconeSala(Player.SalaAtual, sala);

                Player.SalaAtual = sala;
                Player.SalaAtual.SalaExplorada = true;

                RevelarSala(sala);
                RedesenharSalasReveladas();
                SpawnarInimigos(proximaSala);
            }
            Player.podeAndar = true;
        }

        else if (collision.gameObject.CompareTag("EntradaDaDireita"))
        {
            Player.podeAndar = false;
            // AONDE O PLAYER ESTÁ
            Vector2 localizacao = Player.SalaAtual.Localizacao;

            // PARA ONDE ELE VAI
            localizacao += new Vector2(1, 0);

            if (Level.Salas.Exists(x => x.Localizacao == localizacao))
            {
                Sala sala = Level.Salas.First(x => x.Localizacao == localizacao);

                SalasCena.Find(Player.SalaAtual.ID.ToString()).transform.Find(GeradorDeLevel.DeterminarTipoPrefab(Player.SalaAtual))
                .gameObject.SetActive(false);

                GameObject proximaSala = SalasCena.Find(sala.ID.ToString()).transform.Find
                (GeradorDeLevel.DeterminarTipoPrefab(sala)).gameObject;

                proximaSala.SetActive(true);

                // MOVE O PLAYER PARA A PRÓXIMA ENTRADA

                Player.transform.position = new Vector3(-6.2f, 0, 0);

                DestroyGameObjectsWithTag("Enemy");
                DestroyGameObjectsWithTag("Projetil");
                DestroyGameObjectsWithTag("ProjetilInimigo");

                MudarIconeSala(Player.SalaAtual, sala);

                Player.SalaAtual = sala;

                Player.SalaAtual = sala;
                Player.SalaAtual.SalaExplorada = true;

                RevelarSala(sala);
                RedesenharSalasReveladas();
                SpawnarInimigos(proximaSala);
            }
            Player.podeAndar = true;
        }

        else if (collision.gameObject.CompareTag("EntradaDeCima"))
        {
            Player.podeAndar = false;

            // AONDE O PLAYER ESTÁ
            Vector2 localizacao = Player.SalaAtual.Localizacao;

            // PARA ONDE ELE VAI
            localizacao += new Vector2(0, 1);

            if (Level.Salas.Exists(x => x.Localizacao == localizacao))
            {
                Sala sala = Level.Salas.First(x => x.Localizacao == localizacao);

                SalasCena.Find(Player.SalaAtual.ID.ToString()).transform.Find(GeradorDeLevel.DeterminarTipoPrefab(Player.SalaAtual))
                .gameObject.SetActive(false);

                GameObject proximaSala = SalasCena.Find(sala.ID.ToString()).transform.Find
                (GeradorDeLevel.DeterminarTipoPrefab(sala)).gameObject;

                proximaSala.SetActive(true);

                // MOVE O PLAYER PARA A PRÓXIMA ENTRADA

                Player.transform.position = new Vector3(0, -3f, 0);

                DestroyGameObjectsWithTag("Enemy");
                DestroyGameObjectsWithTag("Projetil");
                DestroyGameObjectsWithTag("ProjetilInimigo");

                MudarIconeSala(Player.SalaAtual, sala);

                Player.SalaAtual = sala;

                Player.SalaAtual = sala;
                Player.SalaAtual.SalaExplorada = true;

                RevelarSala(sala);
                RedesenharSalasReveladas();
                SpawnarInimigos(proximaSala);
            }
            Player.podeAndar = true;
        }

        else if (collision.gameObject.CompareTag("EntradaDeBaixo"))
        {

            Player.podeAndar = false;
            // AONDE O PLAYER ESTÁ
            Vector2 localizacao = Player.SalaAtual.Localizacao;

            // PARA ONDE ELE VAI
            localizacao += new Vector2(0, -1);

            if (Level.Salas.Exists(x => x.Localizacao == localizacao))
            {
                Sala sala = Level.Salas.First(x => x.Localizacao == localizacao);

                SalasCena.Find(Player.SalaAtual.ID.ToString()).transform.Find(GeradorDeLevel.DeterminarTipoPrefab(Player.SalaAtual))
                .gameObject.SetActive(false);

                GameObject proximaSala = SalasCena.Find(sala.ID.ToString()).transform.Find
                (GeradorDeLevel.DeterminarTipoPrefab(sala)).gameObject;

                proximaSala.SetActive(true);

                // MOVE O PLAYER PARA A PRÓXIMA ENTRADA

                Player.transform.position = new Vector3(0, 3f, 0);

                DestroyGameObjectsWithTag("Enemy");
                DestroyGameObjectsWithTag("Projetil");
                DestroyGameObjectsWithTag("ProjetilInimigo");

                MudarIconeSala(Player.SalaAtual, sala);

                Player.SalaAtual = sala;

                Player.SalaAtual = sala;
                Player.SalaAtual.SalaExplorada = true;

                RevelarSala(sala);
                RedesenharSalasReveladas();
                SpawnarInimigos(proximaSala);
            }
            Player.podeAndar = true;
        }        
    }

    public static void RevelarSala(Sala S)
    {
        foreach (Sala sala in Level.Salas)
        {
            // ESQUERDA
            if (sala.Localizacao == S.Localizacao + new Vector2(-1, 0))
            {
                sala.SalaRevelada = true;
            }
            // DIREITA
            if (sala.Localizacao == S.Localizacao + new Vector2(1, 0))
            {
                sala.SalaRevelada = true;
            }
            // CIMA
            if (sala.Localizacao == S.Localizacao + new Vector2(0, 1))
            {
                sala.SalaRevelada = true;
            }
            //BAIXO
            if (sala.Localizacao == S.Localizacao + new Vector2(0, -1))
            {
                sala.SalaRevelada = true;
            }

        }
    }

    private void DestroyGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnarInimigos(GameObject sala)
    {
        // Encontrar o GameObject SpawnPoints dentro da sala
        Transform spawnPointsTransform = sala.transform.Find("SpawnPoints");

        // Verificar se o SpawnPoints foi encontrado
        if (spawnPointsTransform != null)
        {
            // Obter todos os pontos de spawn dentro do SpawnPoints
            GameObject[] spawnPoints = new GameObject[spawnPointsTransform.childCount];
            for (int i = 0; i < spawnPointsTransform.childCount; i++)
            {
                spawnPoints[i] = spawnPointsTransform.GetChild(i).gameObject;
            }

            // Para cada ponto de spawn, instanciar um inimigo
            foreach (GameObject spawnPoint in spawnPoints)
            {
                Instantiate(Inimigo, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }
        else
        {
            return;
        }
    }
}
