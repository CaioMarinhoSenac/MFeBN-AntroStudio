using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class GeradorDeLevel : MonoBehaviour
{
    public Sprite[] ArraySpritesMinimapa;
    [SerializeField] private Transform SalasCena;

    /* 
     SALA INICIAL ID:   0
     SALA BOSS ID:      1
     SALA LOJA ID:      2
     SALA TESOURO ID:   3
     SALA NORMAL ID:    6
    */

    void DesenhaNoMinimapa(Sala sala)
    {
        string nome = "Sala";

        if (sala.ID == 0) nome = "SalaInicial";
        if (sala.ID == 1) nome = "SalaBoss";
        if (sala.ID == 2) nome = "SalaLoja";
        if (sala.ID == 3) nome = "SalaTesouro";

        GameObject Mapa = new GameObject(nome);

        Image SalaIcon = Mapa.AddComponent<Image>();
        SalaIcon.sprite = sala.SalaSprite;
        sala.SalaImage = SalaIcon;

        RectTransform rectTransform = SalaIcon.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Level.altura, Level.largura) * Level.escalaIcon;
        rectTransform.position = sala.Localizacao * (Level.escalaIcon * Level.largura * Level.escala + (Level.preenchimento * Level.altura * Level.escala));

        SalaIcon.transform.SetParent(transform, false);

        Level.Salas.Add(sala);
    }

    int NumeroDeSalaAlteatorio()
    {
        return 6;
    }

    bool ChecaSalaExiste(Vector2 localizacao)
    {
        return (Level.Salas.Exists(x => x.Localizacao == localizacao));
    }

    bool ChecaSalaAoRedorExiste(Vector2 localizacao, string direcao)
    {
        switch (direcao)
        {
            case "Direita":
                {
                    // CHECA esquerda, cima e baixo
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x - 1, localizacao.y)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y + 1)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y - 1)))
                    {
                        return true;
                    }
                    break;
                }
            case "Esquerda":
                {
                    // CHECA direita, cima e baixo
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x + 1, localizacao.y)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y + 1)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y - 1)))
                    {
                        return true;
                    }
                    break;
                }
            case "Cima":
                {
                    // CHECA esquerda, direita e baixo
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x - 1, localizacao.y)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x + 1, localizacao.y)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y - 1)))
                    {
                        return true;
                    }
                    break;
                }
            case "Baixo":
                {
                    // CHECA esquerda, cima e direita
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x - 1, localizacao.y)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y + 1)) ||
                        Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x + 1, localizacao.y)))
                    {
                        return true;
                    }
                    break;
                }
        }
        return false;
    }

    bool GerarSalaBoss()
    {
        float maxNum = 0f;
        Vector2 SalaMaisDistante = Vector2.zero;

        foreach(Sala sala in Level.Salas)
        {
            if(Mathf.Abs(sala.Localizacao.x) + Mathf.Abs(sala.Localizacao.y) >= maxNum)
            {
                maxNum = Mathf.Abs(sala.Localizacao.x) + Mathf.Abs(sala.Localizacao.y);

                SalaMaisDistante = sala.Localizacao;
            }
        }

        Sala salaBoss = new Sala();
        salaBoss.SalaSprite = ArraySpritesMinimapa[2];
        salaBoss.ID = 1;

        // ESQUERDA
        if (!ChecaSalaExiste(SalaMaisDistante + new Vector2(-1, 0)))
        {
            if (!ChecaSalaAoRedorExiste(SalaMaisDistante + new Vector2(-1,0), "Direita"))
            {
                salaBoss.Localizacao = SalaMaisDistante + new Vector2(-1, 0);
            }
        }
        // DIREITA
        else if (!ChecaSalaExiste(SalaMaisDistante + new Vector2(1, 0)))
        {
            if (!ChecaSalaAoRedorExiste(SalaMaisDistante + new Vector2(1, 0), "Esquerda"))
            {
                salaBoss.Localizacao = SalaMaisDistante + new Vector2(1, 0);
            }
        }
        // CIMA
        else if (!ChecaSalaExiste(SalaMaisDistante + new Vector2(0, 1)))
        {
            if (!ChecaSalaAoRedorExiste(SalaMaisDistante + new Vector2(0, 1), "Baixo"))
            {
                salaBoss.Localizacao = SalaMaisDistante + new Vector2(0, 1);
            }
        }
        // BAIXO
        else if (!ChecaSalaExiste(SalaMaisDistante + new Vector2(0, -1)))
        {
            if (!ChecaSalaAoRedorExiste(SalaMaisDistante + new Vector2(0, -1), "Cima"))
            {
                salaBoss.Localizacao = SalaMaisDistante + new Vector2(0, -1);
            }
        }

        if (salaBoss.Localizacao == Vector2.zero) {            
            return false;
        }
        else
        {
            DesenhaNoMinimapa(salaBoss);
            return true;
        }
    }

    int auxiliar = 0;

    void GeraSala(Sala sala)
    {
        auxiliar++;

        if (auxiliar > 25)
        {
            return;
        }

        DesenhaNoMinimapa(sala);

        // ESQUERDA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(-1, 0) + sala.Localizacao;
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Direita"))
                {
                    if (Mathf.Abs(novaSala.Localizacao.x) < Level.limiteSalasAxis &&
                        Mathf.Abs(novaSala.Localizacao.y) < Level.limiteSalasAxis)
                    {
                        GeraSala(novaSala);
                    }
                }
            }
        }
        // DIREITA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(1, 0) + sala.Localizacao;
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Esquerda"))
                {
                    if (Mathf.Abs(novaSala.Localizacao.x) < Level.limiteSalasAxis &&
                        Mathf.Abs(novaSala.Localizacao.y) < Level.limiteSalasAxis)
                    {
                        GeraSala(novaSala);
                    }
                }
            }
        }
        // CIMA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(0, 1) + sala.Localizacao;
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Baixo"))
                {
                    if (Mathf.Abs(novaSala.Localizacao.x) < Level.limiteSalasAxis &&
                        Mathf.Abs(novaSala.Localizacao.y) < Level.limiteSalasAxis)
                    {
                        GeraSala(novaSala);
                    }
                }
            }
        }
        // BAIXO
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(0, -1) + sala.Localizacao;
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Cima"))
                {
                    if (Mathf.Abs(novaSala.Localizacao.x) < Level.limiteSalasAxis &&
                        Mathf.Abs(novaSala.Localizacao.y) < Level.limiteSalasAxis)
                    {
                        GeraSala(novaSala);
                    }
                }
            }
        }
    }   

    void ShuffledList<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    bool GeraSalaEspecifica(Sprite salaIcon, int ID)
    {
        List<Sala> ShuffledList = new List<Sala>(Level.Salas);

        Sala salaEspecial = new Sala();
        salaEspecial.SalaSprite = salaIcon;
        salaEspecial.ID = ID;

        bool encontrouLugarDisponivel = false;

        foreach (Sala sala in ShuffledList)
        {
            Vector2 localizacaoSalaEspecial = sala.Localizacao;       
            
            if(sala.ID < 6)
            {
                continue;
            }

            // ESQUERDA
            if (!ChecaSalaExiste(localizacaoSalaEspecial + new Vector2(-1, 0)))
            {
                if (!ChecaSalaAoRedorExiste(localizacaoSalaEspecial + new Vector2(-1, 0), "Direita"))
                {
                    salaEspecial.Localizacao = localizacaoSalaEspecial + new Vector2(-1, 0);
                    encontrouLugarDisponivel = true;
                }
            }
            // DIREITA
            else if (!ChecaSalaExiste(localizacaoSalaEspecial + new Vector2(1, 0)))
            {
                if (!ChecaSalaAoRedorExiste(localizacaoSalaEspecial + new Vector2(1, 0), "Esquerda"))
                {
                    salaEspecial.Localizacao = localizacaoSalaEspecial + new Vector2(1, 0);
                    encontrouLugarDisponivel = true;
                }
            }
            // CIMA
            else if (!ChecaSalaExiste(localizacaoSalaEspecial + new Vector2(0, 1)))
            {
                if (!ChecaSalaAoRedorExiste(localizacaoSalaEspecial + new Vector2(0, 1), "Baixo"))
                {
                    salaEspecial.Localizacao = localizacaoSalaEspecial + new Vector2(0, 1);
                    encontrouLugarDisponivel = true;
                }
            }
            // BAIXO
            else if (!ChecaSalaExiste(localizacaoSalaEspecial + new Vector2(0, -1)))
            {
                if (!ChecaSalaAoRedorExiste(localizacaoSalaEspecial + new Vector2(0, -1), "Cima"))
                {
                    salaEspecial.Localizacao = localizacaoSalaEspecial + new Vector2(0, -1);
                    encontrouLugarDisponivel = true;
                }
            }

            if (encontrouLugarDisponivel)
            {
                DesenhaNoMinimapa(salaEspecial);
                return true;
            }
        }        
        return false;
    }

    void Awake()
    {
        Level.normalRoomIcon = ArraySpritesMinimapa[0];
        Level.atualRoomIcon = ArraySpritesMinimapa[1];
        Level.bossRoomIcon = ArraySpritesMinimapa[2];
        Level.lojaRoomIcon = ArraySpritesMinimapa[3];
        Level.desconhecidoRoomIcon = ArraySpritesMinimapa[4];
        Level.tesouroRoomIcon = ArraySpritesMinimapa[5];
    }
    void Start()
    {        

        // PARA DESENHAR O INÍCIO, SEMPRE NO MEIO DO MAPA:
        Sala salaInicial = new Sala();
        salaInicial.Localizacao = new Vector2(0, 0);
        salaInicial.SalaSprite = Level.atualRoomIcon;
        salaInicial.SalaRevelada = true;
        salaInicial.SalaExplorada = true;
        salaInicial.ID = 0;

        Player.SalaAtual = salaInicial;

        DesenhaNoMinimapa(salaInicial);

        // ESQUERDA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(-1, 0);
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Direita"))
                {
                    GeraSala(novaSala);
                }
            }
        }
        // DIREITA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(1, 0);
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Esquerda"))
                {
                    GeraSala(novaSala);
                }
            }
        }
        // CIMA
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(0, 1);
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Baixo"))
                {
                    GeraSala(novaSala);
                }
            }
        }
        // BAIXO
        if (Random.value > Level.Chance)
        {
            Sala novaSala = new Sala();
            novaSala.Localizacao = new Vector2(0, -1);
            novaSala.SalaSprite = Level.normalRoomIcon;
            novaSala.ID = NumeroDeSalaAlteatorio();

            if (!ChecaSalaExiste(novaSala.Localizacao))
            {
                if (!ChecaSalaAoRedorExiste(novaSala.Localizacao, "Cima"))
                {
                    GeraSala(novaSala);
                }
            }
        }

        bool bossSala = GerarSalaBoss();
        bool lojaSala = GeraSalaEspecifica(Level.lojaRoomIcon, 2);
        bool tesouroSala = GeraSalaEspecifica(Level.tesouroRoomIcon, 3);

        // CRITÉRIOS PARA 'SE NÃO': RECRIAR O MAPA
        if (!lojaSala || !tesouroSala || !bossSala || Level.Salas.Count < 10 || Level.Salas.Count > 20)
        {
            RecriarMapa();
        }
        else
        {
            ControladoraDeSalas.RevelarSala(salaInicial);
            ControladoraDeSalas.RedesenharSalasReveladas();
        }
        // INICIALIZA A SALA INICIAL
        salaInicial = Level.Salas.Find(sala => sala.ID == 0);

        GameObject SalaInicialGO = SalasCena.Find(salaInicial.ID.ToString()).transform.Find
        (DeterminarTipoPrefab(salaInicial)).gameObject;

        SalaInicialGO.SetActive(true);

    }

    void RecriarMapa()
    {
        auxiliar = 0;

        Level.Salas.Clear();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        Start();
    }

    static bool ChecaDirecaoExisteSala(Vector2 localizacao, string direcao)
    {
        switch (direcao)
        {
            case "Direita":
                {
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x + 1, localizacao.y)))
                    {
                        return true;
                    }
                    break;
                }
            case "Esquerda":
                {
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x - 1, localizacao.y)))
                    {
                        return true;
                    }
                    break;
                }
            case "Cima":
                {
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y + 1)))
                    {
                        return true;
                    }
                    break;
                }
            case "Baixo":
                {
                    if (Level.Salas.Exists(x => x.Localizacao == new Vector2(localizacao.x, localizacao.y - 1)))
                    {
                        return true;
                    }
                    break;
                }
        }
        return false;
    }

    public static string DeterminarTipoPrefab(Sala sala)
    {
        // Checa as entradas abertas e determina o tipo de prefab
        bool portaCima = ChecaDirecaoExisteSala(sala.Localizacao, "Cima");
        bool portaBaixo = ChecaDirecaoExisteSala(sala.Localizacao, "Baixo");
        bool portaEsquerda = ChecaDirecaoExisteSala(sala.Localizacao, "Esquerda");
        bool portaDireita = ChecaDirecaoExisteSala(sala.Localizacao, "Direita");

        if (portaCima && !portaBaixo && !portaEsquerda && !portaDireita)
            return "C";

        else if (!portaCima && portaBaixo && !portaEsquerda && !portaDireita)
            return "B";

        else if (portaCima && portaBaixo && !portaEsquerda && !portaDireita)
            return "CB";

        else if (!portaCima && !portaBaixo && portaEsquerda && !portaDireita)
            return "E"; 

        else if (portaCima && !portaBaixo && portaEsquerda && !portaDireita)
            return "CE";

        else if (!portaCima && portaBaixo && portaEsquerda && !portaDireita)
            return "EB";

        else if (portaCima && portaBaixo && portaEsquerda && !portaDireita)
            return "CBE";

        else if (!portaCima && !portaBaixo && !portaEsquerda && portaDireita)
            return "D";

        else if (portaCima && !portaBaixo && !portaEsquerda && portaDireita)
            return "CD";

        else if (!portaCima && portaBaixo && !portaEsquerda && portaDireita)
            return "BD";

        else if (portaCima && portaBaixo && !portaEsquerda && portaDireita)
            return "CBD";

        else if (!portaCima && !portaBaixo && portaEsquerda && portaDireita)
            return "ED";

        else if (portaCima && !portaBaixo && portaEsquerda && portaDireita)
            return "CED";

        else if (!portaCima && portaBaixo && portaEsquerda && portaDireita)
            return "BED";

        else if (portaCima && portaBaixo && portaEsquerda && portaDireita)
            return "CBED";

        else
        {
            return "None";
        }
    }
}