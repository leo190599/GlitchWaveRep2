using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="NovaInformacoesPlayer2",menuName = "ScriptableObjectsCustomizados/InformacoesPlayer2")]
public class InformacoesPlayer2 : ScriptableObject
{
    // Start is called before the first frame update
    public UnityAction EventosAtivacaoGatinho;
    public UnityAction EventosDesativacaoGatinho;
    public void AtivarGatinho()
    {
        if(EventosAtivacaoGatinho!= null)
        {
            EventosAtivacaoGatinho.Invoke();
        }
    }
    public void DesativarGatinho()
    {
        if(EventosDesativacaoGatinho != null)
        {
            EventosDesativacaoGatinho.Invoke();
        }
    }
}
