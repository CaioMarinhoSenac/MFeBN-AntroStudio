using System.Collections;
using UnityEngine;

public class EnemyUSP_S : ArmaDeFogoInimiga
{
    protected override IEnumerator Disparar()
    {
        if (!PodeDisparar())
        {
            yield break;
        }
        else
        {
            cadenciaControl = Time.time + cadencia;

            Instantiate(projetilInimigo, cano.position, cano.rotation);

            GameObject novoProjetil = Instantiate(projetilInimigo, cano.position, cano.rotation);
            Projetil proj = novoProjetil.GetComponent<Projetil>();
            proj.ConfigurarProjetil(danoDoProjetil, velocidadeDoProjetil, duracaoDoProjetil);

            EjetarCartucho();

            somDisparo.Play();

            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            muzzleFlash.SetActive(false);
        }
    }
}
