using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaShotgun : ArmaAutomatica
{
    protected override IEnumerator Disparar()
    {
        foreach (Transform cano in canos)
        {
            cadenciaControl = Time.time + cadencia;
            Instantiate(projetil, cano.position, cano.rotation);
        }
        camAnimations.CamShake();
        municaoAtual--;
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        muzzleFlash.SetActive(false);
    }
    protected override IEnumerator Recarregar()
    {
        municaoAtual = 0;
        recarregando = true;
        animator.SetBool("Recarregando", true);

        RecarregarSlider.SetActive(true);

        Debug.Log("Recarregando...");

        yield return new WaitForSeconds(tempoDeRecarga);
        recarregando = false;
        animator.SetBool("Recarregando", false);

        municaoAtual = municaoMaxima;
        RecarregarSlider.SetActive(false);
    }
}
