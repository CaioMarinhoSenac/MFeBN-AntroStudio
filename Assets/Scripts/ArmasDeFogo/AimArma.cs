using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AimArma : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distancia;
    [SerializeField] SpriteRenderer rendererPlayer;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float distanciaCrosshair;
    [SerializeField] private Animator animatorPlayer;

    private Vector3 playerParaDirecao;
    private float movimentSpeed;

    void Update()
    {
        Aiming();
        Animate();
    }
    void Aiming()
    {
        // ** ARMA **

        // ROTACAO
        var direcao = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3 (0, 0, angulo);

        // POSICAO
        playerParaDirecao = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        playerParaDirecao.z = 0;
        movimentSpeed = Mathf.Clamp(playerParaDirecao.magnitude, 0f, 1f);

        transform.position = player.position + (distancia * playerParaDirecao.normalized);

        // FLIP
        Vector3 escala = Vector3.one;

        if (angulo > 90 ||  angulo < -90)
        {
            escala.y = -1f;            
        }
        else
        {
            escala.y = 1f;
        }

        transform.localScale = escala;

        // ** PLAYER **      
        // ORDER IN LAYER
        if (angulo > 160 || angulo < 20)
        {
            // Define a ordem padrão da camada
            rendererPlayer.sortingOrder = 4;
        }
        else
        {
            rendererPlayer.sortingOrder = 6;
        }

        crosshair.transform.localPosition = playerParaDirecao;
        Cursor.visible = false;
    }

    void Animate()
    {
        // GIRAR SPRITE JOGADOR
        if (playerParaDirecao != Vector3.zero)
        {
            animatorPlayer.SetFloat("Horizontal", playerParaDirecao.x);
            animatorPlayer.SetFloat("Vertical", playerParaDirecao.y);
        }

        animatorPlayer.SetFloat("Speed", movimentSpeed);
    }
}
