using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorBarraVidaPersonagem : MonoBehaviour
{
    [SerializeField]
    private ControladorBarraDeProgresso controladorBarraDeProgresso;
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private TextMeshProUGUI textoVidaPlayer;
    // Start is called before the first frame update
    void Start()
    {
        controladorBarraDeProgresso=GetComponent<ControladorBarraDeProgresso>();
        if(controladorBarraDeProgresso==null)
        {
            Debug.LogError("Coloque esse componente em um objeto com um controlador de barra de progresso");
        }
        else
        {
            AlterarProgresso();
        }
    }

    public void AlterarProgresso()
    {
        controladorBarraDeProgresso.AlterarProgresso(informacoesPlayer.GetPorcentagemDeVida);
        textoVidaPlayer.text="("+informacoesPlayer.GetVidaAtual.ToString()+"%)";
    }
    void OnEnable()
    {
        if(controladorBarraDeProgresso!=null)
        {
            AlterarProgresso();
        }
        informacoesPlayer.EventosLevarDano+=AlterarProgresso;
        informacoesPlayer.EventosCura+=AlterarProgresso;
    }
    void OnDisable()
    {
        informacoesPlayer.EventosLevarDano-=AlterarProgresso;
        informacoesPlayer.EventosCura-=AlterarProgresso;
    }
}
