using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="novoControladorDeCena",menuName ="ScriptableObjectsCustomizados/ControladorDeCena")]
public class ControladorDeCena : ScriptableObject
{

    [SerializeField]
    private TipoEstadoCena EstadoCena;
    // Start is called before the first frame update
    public enum TipoEstadoCena
    {
        jogando=0,
        morreu=1,
        pausado=2,
        venceu=3
    }


    public UnityAction EventosEstadoJogando;

    public UnityAction EventosEstadoMorreu;
    public UnityAction EventosEstadoPausado;
    public UnityAction EventosEstadoVenceu;

    public UnityAction EventosSaidaEstadoAtual;

    public void TrocarEstadoAtual(TipoEstadoCena novoEstadoCena)
    {
        
        switch(novoEstadoCena)
        {
            case TipoEstadoCena.jogando:
                if(EventosSaidaEstadoAtual!=null)
                {
                    EventosSaidaEstadoAtual.Invoke();
                }
                EstadoCena=novoEstadoCena;    
                if(EventosEstadoJogando!=null)
                {
                    EventosEstadoJogando.Invoke();
                }
                Time.timeScale=1;
            break;

            case TipoEstadoCena.morreu:

                if(EventosSaidaEstadoAtual!=null)
                {
                    EventosSaidaEstadoAtual.Invoke();
                }
                EstadoCena=novoEstadoCena;
                if(EventosEstadoMorreu!=null)
                {
                    EventosEstadoMorreu.Invoke();
                }
                Time.timeScale=1;
            break;

            case TipoEstadoCena.pausado:
                
                if(EventosSaidaEstadoAtual!=null)
                {
                    EventosSaidaEstadoAtual.Invoke();
                }
                EstadoCena=novoEstadoCena;
                if(EventosEstadoPausado!=null)
                {
                    EventosEstadoPausado.Invoke();
                }
                Time.timeScale=0;
            break;

            case TipoEstadoCena.venceu:
                if(EventosSaidaEstadoAtual!=null)
                {
                    EventosSaidaEstadoAtual.Invoke();
                }
                EstadoCena=novoEstadoCena;
                if(EventosEstadoVenceu!=null)
                {
                    EventosEstadoVenceu.Invoke();
                }
                Time.timeScale=1;
            break;

            default:
                Debug.LogError("Escolha um estado valido");
            break;
        }
        
    }
    public TipoEstadoCena getEstadoCena=>EstadoCena; 
}
