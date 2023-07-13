using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAbaixadaAtaquePlayer : EstadoAtivoBasePlayer
{
    private float tempoDecorridoNoEstado=0;
    private float tempoMaximoNoEstado;
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.abaixadaAtacando);
    }

    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        tempoDecorridoNoEstado+=Time.deltaTime;
        if(tempoDecorridoNoEstado>tempoMaximoNoEstado+.05)
        {
            EventoFinalAnimacao();
        }
    }

    public override void EventoInicioAnimacao()
    {
        base.EventoInicioAnimacao();
        player.AtivarEfeitosEspada();
        tempoMaximoNoEstado=player.GetAnimator.GetCurrentAnimatorStateInfo(1).length;
    }
    public override void EventoAnimacao()
    {
        base.EventoAnimacao();
        player.AtacarAbaixada();
        player.DesativarEfeitosEspada();
        
    }
    public override void EventoFinalAnimacao()
    {
        base.EventoFinalAnimacao();
        player.TrocaEstadoPlayer(new EstadoAbaixadaPlayer());
    }
    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        player.DesativarEfeitosEspada();
    }
}
