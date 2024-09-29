using UnityEngine;
using UnityEngine.UI;

public class ReloadingScript : MonoBehaviour
{   
    [SerializeField] private Image fill;

    private float tempoDeRecarga, tempo;
    public void ConfigurarTempoDeRecarga(float tempoDeRecarga)
    {
        this.tempoDeRecarga = tempoDeRecarga;
    }
    private void OnEnable()
    {
        tempo = tempoDeRecarga;
        Update();
    }
    void Update()
    {
        tempo -= Time.deltaTime;
        fill.fillAmount = tempo / tempoDeRecarga;

        if (tempo < 0)
        {
            tempo = 0;
        }
    }
}
