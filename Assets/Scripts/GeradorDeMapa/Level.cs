using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static float largura = 500, altura = 500;

    public static float escala = 0.6f;
    public static float escalaIcon = 0.08f;
    public static float preenchimento = 0.07f;

    public static float Chance = 0.5f;

    public static int limiteSalasAxis = 6;

    public static Sprite lojaRoomIcon, desconhecidoRoomIcon, normalRoomIcon, atualRoomIcon, bossRoomIcon, tesouroRoomIcon;

    public static List<Sala> Salas = new List<Sala>();

    public static float cooldownMudaSala = 0.5f;
}

public class Sala
{
    public int ID = 6;
    public Vector2 Localizacao;
    public Image SalaImage;
    public Sprite SalaSprite;
    public bool SalaRevelada;
    public bool SalaExplorada;
}