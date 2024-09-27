using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VidaScript : MonoBehaviour
{
    private void Start()
    {
        DesenharCoracoes();
    }

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

    static void DesenharCoracoes()
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

    public static void ReceberDano(float dano)
    {           
            CoroutineManager.Instance.StartCoroutine(Invencivel(Player.receberDanoCooldown));

            Player.vida -= dano;

            if (Player.vida <= 0)
            {
                CoroutineManager.Instance.StartCoroutine(Morrer());
            }
            else
            {
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

    public static IEnumerator Invencivel(float tempoDeDuracao)
    {
        Player.Hitbox.SetActive(false);
        yield return new WaitForSeconds(tempoDeDuracao);
        Player.Hitbox.SetActive(true);
    }
    public static IEnumerator Morrer()
    {
        Player.animator.SetBool("Morrer", true);
        Player.crosshair.SetActive(false);
        Player.MaoArma.SetActive(false);
        Player.vivo = false;

        yield return new WaitForSeconds(0f);

        for(int i = 0; i < Player.CoracaoPanel.transform.childCount; i++)
        {
            Destroy(Player.CoracaoPanel.transform.GetChild(i).gameObject);
        }

        Destroy(Player.transform.GetComponent<PlayerMoviment>());
        Destroy(Player.transform.GetComponent<ControladoraDeSalas>());
        Destroy(Player.transform.GetComponent<Rigidbody2D>());
        Destroy(Player.transform.GetComponent<Light2D>());
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
