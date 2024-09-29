using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaShotgun : ArmaDeFogo
{
    protected override IEnumerator Disparar()
    {
        foreach (Transform cano in canos)
        {
            cadenciaControl = Time.time + cadencia;

            GameObject novoProjetil = Instantiate(projetil, cano.position, cano.rotation);
            Projetil proj = novoProjetil.GetComponent<Projetil>();
            proj.ConfigurarProjetil(danoDoProjetil, velocidadeDoProjetil, duracaoDoProjetil);
        }

        EjetarCartucho();

        somDisparo.Play();

        camAnimations.CamShake();

        municaoAtual--;

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        muzzleFlash.SetActive(false);
    }
}
