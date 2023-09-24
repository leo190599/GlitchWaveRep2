using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayDano : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private Image overlay;
    // Start is called before the first frame update
    private void OnEnable()
    {
        informacoesPlayer.EventosCura += AtualizarOpacidade;
        informacoesPlayer.EventosLevarDano += AtualizarOpacidade;
        AtualizarOpacidade();
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosCura -= AtualizarOpacidade;
        informacoesPlayer.EventosLevarDano -= AtualizarOpacidade;
    }

    public void AtualizarOpacidade()
    {
        //Debug.Log(informacoesPlayer.GetPorcentagemDeVida);
        overlay.color = new Color(1,1,1,1-(informacoesPlayer.GetPorcentagemDeVida));
    }
}
