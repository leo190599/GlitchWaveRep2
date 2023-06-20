using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtaquePlayer : EstadoAtivoBasePlayer
{
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);

        player.GetRigidbody2D.velocity=new Vector2(0,player.GetRigidbody2D.velocity.y);

        player.Atacar();

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
