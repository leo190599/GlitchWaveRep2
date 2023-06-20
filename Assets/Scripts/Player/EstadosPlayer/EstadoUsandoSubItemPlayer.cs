using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoUsandoSubItemPlayer : EstadoAtivoBasePlayer
{
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=(new Vector2(0,player.GetRigidbody2D.velocity.y));
        Debug.Log("b");
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
