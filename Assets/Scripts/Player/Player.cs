using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    public static float vida;
    public static float vidaMaxAtual;
    public static float vidaMax;

    public static Transform transform;

    public static Sala SalaAtual;

    public static Rigidbody2D rigidbody;

    public static Animator animator;

    public static GameObject CoracaoPanel;
    public static Sprite CoracaoCheio, CoracaoMetade;

    public static GameObject crosshair;
    public static GameObject MaoArma;

    public static GameObject ReloadPanel;

    public static bool vivo;

    public static GameObject Hitbox;

    public static bool podeAndar = true;
}
