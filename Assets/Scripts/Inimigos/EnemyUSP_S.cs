using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyUSP_S : ArmaDeFogoInimiga
{
    protected override IEnumerator Disparar()
    {
        if (!PodeDisparar())
        {
            yield break;
        }
        else if (cano >= canos.Length && canos.Length > 1)
        {
            cano = 0;
            yield break;
        }
        else
        {
            cadenciaControl = Time.time + cadencia;

            Instantiate(projetilInimigo, canos[cano].position, canos[cano].rotation);

            GameObject novoProjetil = Instantiate(projetilInimigo, canos[cano].position, canos[cano].rotation);
            Projetil proj = novoProjetil.GetComponent<Projetil>();
            proj.ConfigurarProjetil(danoDoProjetil, velocidadeDoProjetil, duracaoDoProjetil);

            EjetarCartucho();

            somDisparo.Play();

            cano++;

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
}
