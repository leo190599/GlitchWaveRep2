using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoIdlePlayer : EstadoAtivoBasePlayer
{

    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.SetDadoDashNoAr(false);
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoParado;
        player.GetRigidbody2D.constraints=RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.GetRigidbody2D.velocity=new Vector2(0,0);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.idle);
    }

    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoPulo))
        {
            player.GetRigidbody2D.velocity=new Vector2(player.GetRigidbody2D.velocity.x,player.GetForcaPulo);
            player.TrocaEstadoPlayer(new EstadoPuloPlayer());
            return;
        }
        if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
        {
            player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
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
        if(Input.GetKey(player.GetMapeadorDeBotoes.GetBotaoAbaixar))
        {
            player.TrocaEstadoPlayer(new EstadoAbaixadaPlayer());
            return;
        }
    }
    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        player.GetRigidbody2D.constraints=RigidbodyConstraints2D.FreezeRotation;
    }
}
