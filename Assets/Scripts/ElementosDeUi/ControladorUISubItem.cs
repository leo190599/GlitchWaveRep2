using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUISubItem : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private Image imagemSubItem;
    [SerializeField]
    private TextMeshProUGUI custoSubItem;
    [SerializeField]
    private Color corDesativado = Color.grey;
    [SerializeField]
    private Color corAtivado= Color.white;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AcaoTrocaSubItem();
        informacoesPlayer.EventosTrocaSubItem += AcaoTrocaSubItem;
        informacoesPlayer.EventosCura += AtualizarElementoVisualSubItem;
        informacoesPlayer.EventosLevarDano += AtualizarElementoVisualSubItem;
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosTrocaSubItem -= AcaoTrocaSubItem;
        informacoesPlayer.EventosCura -= AtualizarElementoVisualSubItem;
        informacoesPlayer.EventosLevarDano -= AtualizarElementoVisualSubItem;
    }
    public void AcaoTrocaSubItem()
    {
        if(informacoesPlayer.GetSubItemObjetoScriptavel!=null)
        {
            imagemSubItem.sprite = informacoesPlayer.GetSubItemObjetoScriptavel.GetSimboloSubItem;
            custoSubItem.text = informacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida.ToString();
            AtualizarElementoVisualSubItem();
        }
        else
        {
            imagemSubItem.color = new Color(0,0,0,0);
            custoSubItem.text = "-";
        }
    }
    public void AtualizarElementoVisualSubItem()
    {
        if (informacoesPlayer.GetSubItemObjetoScriptavel != null)
        {
            if (informacoesPlayer.GetVidaAtual > informacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida)
            {
                imagemSubItem.color = corAtivado;
            }
            else
            {
                imagemSubItem.color = corDesativado;
            }
        }
    }
}
