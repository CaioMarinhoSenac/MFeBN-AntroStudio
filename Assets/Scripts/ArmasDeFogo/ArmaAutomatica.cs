using System.Collections;
using UnityEngine;

public class ArmaAutomatica : ArmaDeFogo
{
    protected override IEnumerator Disparar()
    {
        if (cano >= canos.Length && canos.Length > 1)
        {
            cano = 0;
            yield break;
        }
        else
        {
            cadenciaControl = Time.time + cadencia;
            municaoAtual--;

            GameObject novoProjetil = Instantiate(projetil, canos[cano].position, canos[cano].rotation);
            Projetil proj = novoProjetil.GetComponent<Projetil>();
            proj.ConfigurarProjetil(danoDoProjetil, velocidadeDoProjetil, duracaoDoProjetil);

            somDisparo.Play();

            EjetarCartucho();

            camAnimations.CamShake();

            cano++;

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
}
