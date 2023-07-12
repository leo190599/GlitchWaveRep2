using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAbaixadaPlayer : EstadoAtivoBasePlayer
{
    private bool manterDetectorDeRecebimentoDeDanoAbaixada=false;
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=new Vector2(0,player.GetRigidbody2D.velocity.y);  
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.abaixada);
        player.AtivarColisorRecebimentoDeDanoAbaixada();
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAtaque))
        {
            manterDetectorDeRecebimentoDeDanoAbaixada=true;
            player.TrocaEstadoPlayer(new EstadoAbaixadaAtaquePlayer());
            return;
        }
        if(!Input.GetKey(player.GetMapeadorDeBotoes.GetBotaoAbaixar))
        {
            if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
            {
                player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
            }
            else
            {
                player.TrocaEstadoPlayer(new EstadoIdlePlayer());
            }
        }
    }

    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        if(!manterDetectorDeRecebimentoDeDanoAbaixada)
        {
            player.DesativarColisorRecebimentoDeDanoAbaixada();
        }
    }
}
