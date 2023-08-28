using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUIGlitch : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private Image imagemGlitch;
    [SerializeField]
    private TextMeshProUGUI labelCustoGlitch;
    [SerializeField]
    private Color corDesativado = Color.grey;
    [SerializeField]
    private Color corAtivado = Color.white;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AtualizarElementoVisualGlitch();
        informacoesPlayer.EventosCura += AtualizarElementoVisualGlitch;
        informacoesPlayer.EventosLevarDano += AtualizarElementoVisualGlitch;
        labelCustoGlitch.text = informacoesPlayer.GetDanoGlitchPlayer.ToString();
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosCura -= AtualizarElementoVisualGlitch;
        informacoesPlayer.EventosLevarDano -= AtualizarElementoVisualGlitch;
    }
    public void AtualizarElementoVisualGlitch()
    {
        if(informacoesPlayer.GetVidaAtual>informacoesPlayer.GetDanoGlitchPlayer)
        {
            imagemGlitch.color = corAtivado;
        }
        else
        {
            imagemGlitch.color = corDesativado;
        }
    }
}
