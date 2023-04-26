using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EstadoBasePlayer : EstadoRaiz
{
    protected ScriptPlayer player;
    public override void IniciarEstado()
    {
        base.IniciarEstado();
        Debug.LogError("Inicie o estado com IniciarEstadoPlayer");
    }

    public virtual void IniciarEstadoPlayer(ScriptPlayer player)
    {
        this.player= player;
    }
    public virtual void EventoInicioAnimacao()
    {

    }

    public virtual void EventoAnimacao()
    {

    }

    public virtual void EventoFinalAnimacao()
    {
        
    }
}
