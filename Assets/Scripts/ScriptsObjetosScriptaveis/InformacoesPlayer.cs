using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="NovasInformacoesPlayer",menuName ="ScriptableObjectsCustomizados/InformacoesPlayer")]
public class InformacoesPlayer : ScriptableObject
{
    [SerializeField]
    private float vidaMaxima=100;
    [SerializeField]
    private float vidaAtual=100;
    [SerializeField]
    private SubItemObjetoScriptavel subItemObjetoScriptavel;

    [SerializeField]
    private float danoAtaqueBasico=10;
    public UnityAction EventosLevarDano;
    public UnityAction EventosCura;
    public UnityAction EventosMorte;

    public float GetPorcentagemDeVida=>Mathf.Clamp(vidaAtual/vidaMaxima,0f,100f);
    public float GetVidaAtual=>Mathf.Clamp(vidaAtual,0,vidaMaxima);
    public float GetVidaMaxima=>vidaMaxima;

    public void EncherVida()
    {
        vidaAtual=vidaMaxima;
        if(EventosCura!=null)
        {
            EventosCura.Invoke();
        }
    }
    public void Curar(float quantidadeDeCura)
    {
        vidaAtual=Mathf.Clamp(vidaAtual+quantidadeDeCura,0,vidaMaxima);
        if(EventosCura!=null)
        {
            EventosCura.Invoke();
        }
    }
    public void ReceberDano(float quantidadeDeDano)
    {
        vidaAtual-=quantidadeDeDano;
        if(EventosLevarDano!=null)
            {
                EventosLevarDano.Invoke();
            }
        if(vidaAtual<=0 && EventosMorte!=null)
        {

            EventosMorte.Invoke();
        }
    }
    public void SetSubItemObjetoScriptavel(SubItemObjetoScriptavel subItemObjetoScriptavel)
    {
        this.subItemObjetoScriptavel=subItemObjetoScriptavel;
    }

    public SubItemObjetoScriptavel GetSubItemObjetoScriptavel=>subItemObjetoScriptavel;

    public GameObject GetPrefabSubItem()
    {
        if(subItemObjetoScriptavel!=null)
        {
            return subItemObjetoScriptavel.GetPrefabSubItem;
        }
        return null;
    }

    public float GetDanoAtaqueBasico=>danoAtaqueBasico;
}
