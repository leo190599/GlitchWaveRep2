using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarraVidaPersonagem : MonoBehaviour
{
    [SerializeField]
    private ControladorBarraDeProgresso controladorBarraDeProgresso;
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
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
