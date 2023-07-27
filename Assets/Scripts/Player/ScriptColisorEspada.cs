using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptColisorEspada : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    private MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
    void OnTriggerEnter2D(Collider2D c)
    {
        mensageiroDeEntradaDeTriggerDanoPlayerInimigo=c.gameObject.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
        if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null)
        {
            mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(informacoesPlayer.GetDanoAtaqueBasico);
        }
    }
}
