using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VidaScript : MonoBehaviour
{
    public static void DesenharCoracao(Sprite tipo, int num)
    {
        GameObject Coracao = new GameObject("Coração");
        Image CoracaoIcon = Coracao.AddComponent<Image>();

        CoracaoIcon.sprite = tipo;

        RectTransform rectTransform = Coracao.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Player.CoracaoPanel.GetComponent<RectTransform>().sizeDelta.x/10,
                                              Player.CoracaoPanel.GetComponent<RectTransform>().sizeDelta.y / 10);
        float Xpos;
        float Ypos = -5;
        if (num > 9)
        {
            Xpos = num * Player.CoracaoPanel.GetComponent<RectTransform>().sizeDelta.x / 10;
        }
        else
        {
            Xpos = (num - 10) * Player.CoracaoPanel.GetComponent<RectTransform>().sizeDelta.x / 10;
            Ypos = -5 - Player.CoracaoPanel.GetComponent<RectTransform>().sizeDelta.x / 10;
        }
        rectTransform.position = new Vector2(Xpos, Ypos);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);

        CoracaoIcon.transform.SetParent(Player.CoracaoPanel.transform, false);

        CoracaoIcon.preserveAspect = true;
    }

    public static void DesenharCoracoes()
    {
        for (int i = 0; i < Mathf.FloorToInt(Player.vida); i++)
        {
            DesenharCoracao(Player.CoracaoCheio, i);
        }

        // Se a parte decimal da vida for maior que 0, desenhar um coração pela metade
        float decimalPart = Player.vida - Mathf.Floor(Player.vida);
        if (decimalPart > 0)
        {
            DesenharCoracao(Player.CoracaoMetade, Mathf.FloorToInt(Player.vida));
        }
    }

    public static void AtivarInvencibilidade(float duracao)
    {
        CoroutineManager.Instance.StartCoroutine(Invencivel(duracao));
    }

    public static IEnumerator Invencivel(float cooldown)
    {
        Player.invencivel = true; // Ativa a invencibilidade

        yield return new WaitForSeconds(cooldown); // Aguarda a duração da invencibilidade

        Player.invencivel = false; // Desativa a invencibilidade
    }

    public static void ReceberDano(float dano)
    {
        if (Player.invencivel) return;

        Player.vida -= dano;

        if (Player.vida <= 0)
            {
                Morrer();
            }
            else
            {
                AtivarInvencibilidade(Player.receberDanoCooldown);
                RedesenharCoracoes();
            }       
    }

    public static void ReceberVida(float quantidade, string tipo)
    {
        if(tipo == "VidaMax")
        {
            if(Player.vidaMaxAtual < Player.vidaMax)
            {
                Player.vidaMaxAtual += quantidade;
            }
            else
            {
                return;
            }
        }
        else if(tipo == "VidaAtual")
        {
            if(Player.vida <  Player.vidaMax)
            {
                Player.vida += quantidade;
            }
            else
            {
                return;
            }
        }       
    }
    public static void Morrer()
    {
        Player.animator.SetBool("Morrer", true);

        for (int i = 0; i < Player.CoracaoPanel.transform.childCount; i++)
        {
            Destroy(Player.CoracaoPanel.transform.GetChild(i).gameObject);
        }

        Player.crosshair.SetActive(false);
        Player.MaoArma.SetActive(false);
        Player.vivo = false;
        Player.podeAndar = false;
    }
    public static void RedesenharCoracoes()
    {
        for (int i = 0; i < Player.CoracaoPanel.transform.childCount; i++)
        {
            Destroy(Player.CoracaoPanel.transform.GetChild(i).gameObject);
        }
        DesenharCoracoes();
    }
    
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;

        public static CoroutineManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    GameObject gameObject = new GameObject("CoroutineManager");
                    _instance = gameObject.AddComponent<CoroutineManager>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            _instance = this;
        }

    }
}
