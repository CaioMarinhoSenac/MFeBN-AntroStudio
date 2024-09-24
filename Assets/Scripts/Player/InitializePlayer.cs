using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody;   
    public GameObject PlayerCoracaoPanel;
    public Sprite PlayerCoracaoCheio, PlayerCoracaoMetade;
    public Animator playerAnimator;
    public float playerVida;
    public static bool playerInvencivel ;
    public GameObject playerCrosshair;
    public GameObject playerMaoArma;
    public GameObject playerReloadPanel;
    public GameObject playerHitbox;

    private void Start()
    {
        Player.transform = playerTransform;
        Player.rigidbody = playerRigidbody;
        Player.vida = playerVida;
        Player.vidaMax = 12f;
        Player.vidaMaxAtual = playerVida;
        Player.CoracaoPanel = PlayerCoracaoPanel;
        Player.CoracaoMetade = PlayerCoracaoMetade;
        Player.CoracaoCheio = PlayerCoracaoCheio;
        Player.animator = playerAnimator;
        Player.crosshair = playerCrosshair;
        Player.MaoArma = playerMaoArma;
        Player.vivo = true;
        Player.ReloadPanel = playerReloadPanel;
        Player.Hitbox = playerHitbox;
    }
}
