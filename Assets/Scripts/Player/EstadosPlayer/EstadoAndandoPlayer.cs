using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAndandoPlayer : EstadoAtivoBasePlayer
{
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
         player.GetRigidbody2D.velocity=new Vector2(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,player.GetRigidbody2D.velocity.y);
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoAndando;
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.correndo);
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
        {
            player.RodarPersonagem(Mathf.Sign(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal))>0);
            player.GetRigidbody2D.velocity=new Vector2(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,player.GetRigidbody2D.velocity.y);
        }
        else
        {
            player.TrocaEstadoPlayer(new EstadoIdlePlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoPulo))
        {
           player.GetRigidbody2D.AddForce(new Vector2(0,player.GetForcaPulo),ForceMode2D.Impulse);
            player.TrocaEstadoPlayer(new EstadoPuloPlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAtaque))
        {
            player.TrocaEstadoPlayer(new EstadoAtaquePlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoSubItem))
        {
            player.TrocaEstadoPlayer(new EstadoUsandoSubItemPlayer());
            return;
        }
    }
}
