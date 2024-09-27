using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladoraDeSalas : MonoBehaviour
{
    public Transform SalasCena;
    private Sprite anteriorIcon;
    public GameObject Inimigo;
    public InitializePlayer initializePlayer;

    void Start()
    {
        anteriorIcon = Level.normalRoomIcon;
    }
    void Update()
    {
        if (!Player.vivo)
        {
            Player.ReloadPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Renascer();
            }
        }
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

                DestruirObjetos();

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

                DestruirObjetos();

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

                DestruirObjetos();

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

                DestruirObjetos();

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

    void Renascer()
    {
            // Impede que o jogador ande durante a transição
            Player.podeAndar = false;

            // Define a localização da sala inicial
            Vector2 localizacaoInicial = new Vector2(0, 0); // Supondo que (0, 0) é a localização da sala inicial

            // Verifica se a sala inicial existe nas salas do nível
            if (Level.Salas.Exists(x => x.Localizacao == localizacaoInicial))
            {
                Sala salaInicial = Level.Salas.First(x => x.Localizacao == localizacaoInicial);

                // Desativa a sala atual
                SalasCena.Find(Player.SalaAtual.ID.ToString()).transform.Find(GeradorDeLevel.DeterminarTipoPrefab(Player.SalaAtual))
                .gameObject.SetActive(false);

                // Ativa a sala inicial
                GameObject salaInicialObj = SalasCena.Find(salaInicial.ID.ToString()).transform.Find
                (GeradorDeLevel.DeterminarTipoPrefab(salaInicial)).gameObject;

                salaInicialObj.SetActive(true);                

                // Atualiza o ícone da sala
                MudarIconeSala(Player.SalaAtual, salaInicial);               

                // Revela a sala inicial e redesenha as salas reveladas
                RedesenharSalasReveladas();                               

                // Reajusta o Player
                initializePlayer.ReinitializePlayer();

            // Atualiza a sala atual do jogador
            Player.SalaAtual = salaInicial;

            // Remove inimigos e projéteis da sala anterior
            DestruirObjetos();
        }
    }

    void DestruirObjetos()
    {
        DestroyGameObjectsWithTag("Enemy");
        DesativarObjetosPorTag("Projetil");
        DesativarObjetosPorTag("ProjetilInimigo");
    }
    void DesativarObjetosPorTag(string tag)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(false);
        }
    }
}
