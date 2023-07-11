using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAbaixadaPlayer : EstadoAtivoBasePlayer
{
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=new Vector2(0,player.GetRigidbody2D.velocity.y);  
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.abaixada);
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAtaque))
        {
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
}
