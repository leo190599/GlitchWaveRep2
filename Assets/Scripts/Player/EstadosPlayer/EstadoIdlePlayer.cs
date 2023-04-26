using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoIdlePlayer : EstadoAtivoBasePlayer
{

    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=new Vector2(0,player.GetRigidbody2D.velocity.y);
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoParado;
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoPulo))
        {
            player.GetRigidbody2D.AddForce(new Vector2(0,player.GetForcaPulo),ForceMode2D.Impulse);
            player.TrocaEstadoPlayer(new EstadoPuloPlayer());
            return;
        }
        if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
        {
            player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
        }
    }
}
