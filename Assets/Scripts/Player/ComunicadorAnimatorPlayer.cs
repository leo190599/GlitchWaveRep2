using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComunicadorAnimatorPlayer : MonoBehaviour
{
    [SerializeField]
    private ScriptPlayer player;
    
    public void EventoInicioAnimacao()
    {
        player.AtivarEventoInicioAnimacao();
    }
    public void EventoAnimacao()
    {
        player.AtivarEventoAnimacao();
    }

    public void EventoFinalAnimacao()
    {
        player.AtivarEventoFinalAnimacao();
    }
}
